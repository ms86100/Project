using Brothers.Entities.DataAccess;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Brothers.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class EnquiryController : Controller
    {
        // GET: Enquiry
        public dalGenEnquiry db = new dalGenEnquiry();
        public ActionResult List(DateTime? EnquiryDate, int PageNo = 1, int PageSize = 10)
        {
            MstEnquiryViewModel obj = new MstEnquiryViewModel();
            int TotalRow;
            obj.MstEnquiryList = db.GetEnquiryPaged(EnquiryDate,PageNo, PageSize, out TotalRow);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_pvEnquiryList", obj);
            }
            return View(obj);
        }

        public ActionResult View(long id)
        {
            var model = db.GetEnquiryByID(id);
            return PartialView("_pvEnquiryDetails", model);
        }
        public ActionResult Reply(long id)
        {
            MstEnquiryReplyModel obj = new MstEnquiryReplyModel();
            obj.utblGenEnquiries = db.GetEnquiryByID(id);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(MstEnquiryReplyModel paramdata)
        {
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
            //Create the SMTP Client
            SmtpClient client = new SmtpClient();
            client.Host = settings.Smtp.Network.Host;
            client.Credentials = credential;
            client.Timeout = 300000;
            client.EnableSsl = false;

            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(settings.Smtp.Network.UserName, "Brothers Tours & Travel");
                mail.To.Add(paramdata.utblGenEnquiries.GuestEmailID);
                mail.Priority = MailPriority.High;
                mail.Subject = paramdata.utblMails.Subject;
                mail.Body = paramdata.utblMails.MessageBody;
                try
                {
                    client.Send(mail);
                    paramdata.utblGenEnquiries.EnquiryRepliedOn = DateTime.Now;
                    paramdata.utblGenEnquiries.EnquiryStatus = "Replied";
                    paramdata.utblGenEnquiries.EnquiryRepliedBy = User.Identity.GetUserName().ToString();
                    db.Save(paramdata.utblGenEnquiries);
                    TempData["ErrMsg"] = 1;
                    return RedirectToAction("list");

                }
                catch (Exception ex)
                {
                    TempData["ErrMsg"] = 0;
                    return RedirectToAction("list");

                }
            }
            paramdata.utblGenEnquiries = db.GetEnquiryByID(paramdata.utblGenEnquiries.EnquiryID);
            return View(paramdata);
        }
    }
}