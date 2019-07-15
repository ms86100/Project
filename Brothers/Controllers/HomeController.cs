using Brothers.Entities.DataAccess;
using Brothers.Entities.ViewModels;
using Brothers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Brothers.Controllers
{
    public class HomeController : Controller
    {
        dalMstDestination dbDest = new dalMstDestination();
        dalMstTourPackagePhoto dbPhoto = new dalMstTourPackagePhoto();
        dalMstTourPackage dbTour = new dalMstTourPackage();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MountPandimTour()
        {
            MstTourPackageGeneralViewAndSearch obj = new MstTourPackageGeneralViewAndSearch();
            dalMstPackageType dbType = new dalMstPackageType();
            dalBlog objDal = new dalBlog();

            int TotalRow;
            //obj.MstDestinationList = dbDest.MstDestinationList;
            //obj.MstPackageTypeList = dbType.MstPackageList;
            //obj.MstDurationList = dbTour.GetDurationList();
            obj.MstTourPackageList = dbTour.GetTopTenPackages();
            obj.BlogList = objDal.BlogGetPaged(1, 4, out TotalRow, "");
            return View(obj);

        }


        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ContactModel objModel = new ContactModel();
            return View(objModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactModel model)
        {
            if(ModelState.IsValid)
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
                
                MailMessage mail = new MailMessage();
                StringBuilder mailbody = new StringBuilder();
                mail.From = new MailAddress("info@mountpandim.com");
                mail.To.Add("deeneo3@gmail.com");
                mail.Priority = MailPriority.High;
               
                mail.CC.Add("ms86100@gmail.com");
                mail.Subject = "Message from Customer";
                mailbody.Append("Name: "+model.FirstName+" "+model.LastName +" Email: "+model.EmailAddress+" "+" "+" Message: " +model.Message );
                mailbody.Append("<br/>"+model.PhoneNumber);
                mail.Body = mailbody.ToString();
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                    return RedirectToAction("contact");
                }
                catch (Exception ex)
                {
                    TempData["ErrMsg"] = 0;
                    return RedirectToAction("contact");
                }

            }
            return View(model);
        }
        public ActionResult rough()
        {
           
            return View();
        }
        [WordDocument]
        public ActionResult AboutDocument()
        {
            ViewBag.WordDocumentFilename = "AboutMeDocument";
            return View("rough");
        }
    }
}