using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brothers.Entities.DataAccess;
using Brothers.Entities.ViewModels;
using Brothers.Entities.Models;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Net.Mail;

namespace Brothers.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class BookingRequestController : Controller
    {
        public dalTourPackageBooking dbBook = new dalTourPackageBooking();
        public ActionResult List(int PageNo = 1, int PageSize = 10, string searchterm = null)
        {
            MstBookingViewModel obj = new MstBookingViewModel();
            int TotalRow;
            obj.MstBookingList = dbBook.GetBookingPaged(PageNo, PageSize, out TotalRow, searchterm);
            obj.pagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("pvBookingList", obj);
            }
            return View(obj);
        }
        [WordDocument]
        public ActionResult Export(long id, long packid)
        {
            dalMstTourPackageActivity dbAct = new dalMstTourPackageActivity();
            dalMstTourPackage dbTour = new dalMstTourPackage();
            dalTourPackageBooking dbBook = new dalTourPackageBooking();
            MstBookingExportModel obj = new MstBookingExportModel();
            obj.MstTourActivityList = dbAct.MstTourPackageActivityList(packid);
            obj.MstTourPackages = dbTour.MstTourPackageView(packid);
            obj.MstTourBooking = dbBook.GetBookingByID(id);
            ViewBag.WordDocumentFilename = obj.MstTourPackages.PackageName;
            return View("Export", obj);
        }
        public ActionResult Reply(long id, long packid)
        {
            MstBookingReplyModel obj = new MstBookingReplyModel();
            obj.MstTourBook = dbBook.GetBookingByID(id);
            dalMstTourPackage dbPack = new dalMstTourPackage();
            MstTourPackageDetailsModel pack = new MstTourPackageDetailsModel();
            pack = dbPack.GetTourPackageDetailsByID(packid);
            ViewBag.PackageName = pack.PackageName;
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(MstBookingReplyModel paramdata)
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
                mail.To.Add(paramdata.MstTourBook.ClientEmailID);
                mail.Priority = MailPriority.High;
                mail.Subject = paramdata.MstMail.Subject;
                mail.Body = paramdata.MstMail.MessageBody;
                mail.ReplyToList.Add("brothersreservation@gmail.com");
                try
                {
                    client.Send(mail);
                    paramdata.MstTourBook.Status = "Replied";
                    dbBook.Save(paramdata.MstTourBook);
                    TempData["ErrMsg"] = 1;
                    return RedirectToAction("list");

                }
                catch (Exception ex)
                {
                    TempData["ErrMsg"] = 0;
                    return RedirectToAction("list");

                }
            }
            paramdata.MstTourBook = dbBook.GetBookingByID(paramdata.MstTourBook.BookingID);
            MstTourPackageDetailsModel pack = new MstTourPackageDetailsModel();
            dalMstTourPackage dbPack = new dalMstTourPackage();
            pack = dbPack.GetTourPackageDetailsByID(paramdata.MstTourBook.PackageID);
            ViewBag.PackageName = pack.PackageName;
            return View(paramdata);
        }
    }
}