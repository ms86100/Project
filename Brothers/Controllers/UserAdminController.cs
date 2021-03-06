﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;

using Brothers.Entities.ViewModels;
using Brothers.Models;
using System.Text.RegularExpressions;

namespace Brothers.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAdminController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        #region Membership Initialization
        public UserAdminController()
        {
        }
        public UserAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        #region User(create, list)
        public ActionResult List(int PageNo = 1, int PageSize = 10, string searchterm = null)
        {
            int TotalRow;
            UserListViewModel objUsers = new UserListViewModel();
            if (searchterm != null)
            {
                searchterm = searchterm.Trim();
                searchterm = Regex.Replace(searchterm, @"\s+", " ");
            }
            objUsers.UserList = UserManager.Users.Where(x => searchterm == null || x.UserName.StartsWith(searchterm)).OrderByDescending(x => x.UserName).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
            TotalRow = objUsers.UserList.Count();
            objUsers.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("pvUserList", objUsers);
            }
            return View(objUsers);
        }
        public ActionResult Create()
        {
            AddRolesToViewData();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string UserBy)
        {

            string TempPassword = "Password";//Membership.GeneratePassword(6, 0);
            if (ModelState.IsValid)
            {
                String UserName = userViewModel.Email;
                UserName = UserName.Trim();
                UserName = Regex.Replace(UserName, @"\s+", " ");
                var user = new ApplicationUser
                {
                    UserName = UserName,
                    Email = userViewModel.Email,
                    PhoneNumber = userViewModel.PhoneNumber,
                };
                var adminresult = await UserManager.CreateAsync(user, TempPassword);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    var result = await this.UserManager.AddToRolesAsync(user.Id, userViewModel.UserRole);
                    if (result.Succeeded)
                    {

                        int MailStatus = 0;//SendEmailConfirmationToken(user.Id, "Confirm your account", user.Email, TempPassword);
                        if (MailStatus == 0)
                        {
                            TempData["MailMsg"] = 1;
                            return RedirectToAction("list");
                        }
                        else
                        {
                            TempData["MailMsg"] = 2;
                            return RedirectToAction("list");
                        }
                        //return RedirectToAction("list");
                    }
                    else
                    {
                        TempData["ErrMsg"] = 0;
                        AddRolesToViewData();
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                }
                else
                {
                    TempData["ErrMsg"] = 0;
                    AddRolesToViewData();
                    ModelState.AddModelError("", "Unable to add user...");
                    return View();
                }
            }
            else
            {
                AddRolesToViewData();
            }
            return View();
        }

        private void AddRolesToViewData()
        {
            var rolesList = new List<RoleViewModel>();
            foreach (var role in _db.Roles)
            {
                var roleModel = new RoleViewModel(role);
                rolesList.Add(roleModel);
            }
            var list = new SelectList(rolesList, "RoleName", "RoleName");
            ViewBag.Roles = list;
        }
        #endregion User(create)

        #region Send Email Confirmation Code block
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResendEmail(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);
            if (user == null)
            {
                TempData["ErrMsg"] = 0;
                return RedirectToAction("list");
            }
            string TempPassword = "password"; //Membership.GeneratePassword(6, 0);
            string token = UserManager.GeneratePasswordResetToken(Id);
            var result = await UserManager.ResetPasswordAsync(Id, token, TempPassword);

            if (result.Succeeded)
            {
                int MailStatus = 0;// SendEmailConfirmationToken(Id, "Confirm your account", user.Email, TempPassword);
                if (MailStatus == 0)
                {
                    TempData["MailMsg"] = 1;
                    return RedirectToAction("list", "useradmin");
                }
                else
                {
                    TempData["MailMsg"] = 0;
                    return RedirectToAction("list", "useradmin");
                }
            }
            else
            {
                TempData["ErrMsg"] = 0;
                return RedirectToAction("list", "useradmin");
            }
        }

        private int SendEmailConfirmationToken(string userID, string subject, string email, string pass)
        {
            string code = UserManager.GenerateEmailConfirmationToken(userID);
            //string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userID, code = code, area = "" }, protocol: Request.Url.Scheme);

            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
            //Create the SMTP Client
            SmtpClient client = new SmtpClient();
            client.Host = settings.Smtp.Network.Host;
            client.Credentials = credential;
            client.Timeout = 300000;
            client.EnableSsl = false;

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(settings.Smtp.Network.UserName, "Mount Pandim Tours & Travel");

            StringBuilder mailbody = new StringBuilder();

            mm.To.Add(email);
            mm.Priority = MailPriority.High;

            mm.Subject = "Confirm your account";
            mm.Body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a> <br /> Login using the following details: <br /> User Name: " + email + "<br /> Password: " + pass;
            mm.IsBodyHtml = true;

            try
            {
                client.Send(mm);
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
        #endregion

        #region Delete User
        //
        // POST: /UserAdmin/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, int PgNo, int PgSize, int ListCount)
        {
            if (id == User.Identity.GetUserId())
            {
                TempData["ErrMsg"] = 4;
            }
            else if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    TempData["ErrMsg"] = 0;
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                TempData["ErrMsg"] = 1;
                ListCount--;
                if (ListCount == 0)
                    PgNo = 1;
            }
            return RedirectToAction("List", new { PageNo = PgNo, PageSize = PgSize });
        }
        #endregion
    }
}