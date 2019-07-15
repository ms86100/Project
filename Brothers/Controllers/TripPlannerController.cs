using Brothers.Entities.DataAccess;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Brothers.Controllers
{
    public class TripPlannerController : Controller
    {
        dalMstDestination dbDest = new dalMstDestination();
        dalMstTourPackagePhoto dbPhoto = new dalMstTourPackagePhoto();
        dalMstTourPackage dbTour = new dalMstTourPackage();
        //
        // GET: /TripPlanner/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TourPackages()
        {
            MstTourPackageGeneralViewAndSearch obj = new MstTourPackageGeneralViewAndSearch();
            dalMstPackageType dbType = new dalMstPackageType();
            obj.MstDestinationList = dbDest.MstDestinationList;
            obj.MstPackageTypeList = dbType.MstPackageList;
            obj.MstDurationList = dbTour.GetDurationList();
            obj.MstTourPackageList = dbTour.GetTopTenPackages();
            return View("TourPackages", obj);
        }
        public ActionResult SearchTourPackages(List<long> Destination, List<String> Duration, List<long> PackType)
        {
            string destIds = Destination != null ? String.Join("|", Destination.ToArray()) : null;
            string duration = Duration != null ? String.Join("|", Duration.ToArray()) : null;
            string typeIds = PackType != null ? typeIds = String.Join("|", PackType.ToArray()) : null;
            string url = Url.Action("SearchList", new { dest = destIds, du = duration, t = typeIds });
            return Json(new { success = true, url = url });
        }
        public ActionResult SearchList(string dest, string du, string t)
        {
            MstTourPackageGeneralViewAndSearch obj = new MstTourPackageGeneralViewAndSearch();
            obj.MstTourPackageList = dbTour.SearchTourPackage(dest, du, t);
            return PartialView("pvTourPackages", obj);
        }
        public ActionResult TourPackageDetails(long id)
        {
            dbTour.RaiseHitCount(id);
            dalMstTourPackageActivity dbAct = new dalMstTourPackageActivity();
            dalTourPackageMap dbMap = new dalTourPackageMap();
            MstPackageGeneralViewModel obj = new MstPackageGeneralViewModel();
            obj.MstTourPhotoList = dbPhoto.MstTourPackagePhotoList(id);
            obj.MstTourActivityList = dbAct.MstTourPackageActivityList(id);
            obj.MstTourPackage = dbTour.MstTourPackageView(id);
            obj.MstTourMap = dbMap.GetTourMapByID(id);
            return View("TourPackageDetails", obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookingRequest(MstPackageGeneralViewModel model)
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
            dalTourPackageBooking dbBook = new dalTourPackageBooking();
            model.MstPackageBooking.PackageID = model.MstTourPackage.PackageID;
            DateTime Arrdate = model.MstPackageBooking.ArrivalDate;
            DateTime Departuredate = model.MstPackageBooking.DepartureDate;
            string tempdt = Arrdate.ToString("dd MMM yyyy");
            string Deptdt = Departuredate.ToString("dd MMM yyyy");
            model.MstPackageBooking.ArrivalDate = DateTime.ParseExact(tempdt, "dd MMM yyyy", CultureInfo.InvariantCulture);
            model.MstPackageBooking.DepartureDate = DateTime.ParseExact(Deptdt, "dd MMM yyyy", CultureInfo.InvariantCulture);
            
            DateTime Currdate = DateTime.Today;
            string tempdate = Currdate.ToString("dd MMM yyyy");
            string Depdate = Currdate.ToString("dd MMM yyyy");
            DateTime NewCurrDate = DateTime.ParseExact(tempdate, "dd MMM yyyy", CultureInfo.InvariantCulture);
            DateTime DepartureDate = DateTime.ParseExact(Depdate, "dd MMM yyyy", CultureInfo.InvariantCulture);


            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                StringBuilder mailbody = new StringBuilder();
                mail.From = new MailAddress("info@mountpandim.com", "Pandim Tours & Travel");
                //mail.To.Add("brothersreservation@gmail.com");
                mail.To.Add(model.MstPackageBooking.ClientEmailID);
                mail.Priority = MailPriority.High;
                mail.ReplyToList.Add("deeneo3@gmail.com");
                mail.CC.Add("ms86100@gmail.com");
                mail.Subject = "Booking Request for Tour Package: " + model.MstTourPackage.PackageName;
                mailbody.AppendLine("Thank You for your Booking Request for the Tour Package: <b>" + model.MstTourPackage.PackageName + "</b>.<br/> We will get Back to you with further details as soon as possible.");
                string packdetails = MakeMailBody(model.MstTourPackage.PackageID);
                mailbody.Append(packdetails);
                mailbody.Append("<h4>Personal Details</h4>");
                mailbody.Append("Name: " + model.MstPackageBooking.ClientName + "<br/>Total number of people: " + model.MstPackageBooking.AdultPax + " Adults / " + model.MstPackageBooking.ChildPax + " Child / " + model.MstPackageBooking.InfantPax + " Infants.<br/>");
                mailbody.Append("Arrival Date: " + model.MstPackageBooking.ArrivalDate.ToString("dd MMM yyyy") + "<br/>Contact No: " + model.MstPackageBooking.ClientContactNo);
                mailbody.Append("Arrival Date: " + model.MstPackageBooking.DepartureDate.ToString("dd MMM yyyy"));
                mailbody.AppendLine("<br/>Requirement: " + model.MstPackageBooking.ClientRequirement);
                mailbody.Append("<br/>Please check your mail for regular updates from us.");
                mail.Body = mailbody.ToString();
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                    model.MstPackageBooking.BookingDate = NewCurrDate;
                    model.MstPackageBooking.Status = "Not Replied";
                    dbBook.Save(model.MstPackageBooking);
                    return RedirectToAction("RequestSuccess", new { id = model.MstTourPackage.PackageID });
                }
                catch (Exception)
                {
                    TempData["ErrMsg"] = 0;
                    return RedirectToAction("TourPackageDetails", new { id = model.MstTourPackage.PackageID });
                }
            }
            dalMstTourPackageActivity dbAct = new dalMstTourPackageActivity();
            dalTourPackageMap dbMap = new dalTourPackageMap();
            model.MstTourPhotoList = dbPhoto.MstTourPackagePhotoList(model.MstTourPackage.PackageID);
            model.MstTourPackage = dbTour.MstTourPackageView(model.MstTourPackage.PackageID);
            model.MstTourActivityList = dbAct.MstTourPackageActivityList(model.MstTourPackage.PackageID);
            model.MstTourMap = dbMap.GetTourMapByID(model.MstTourPackage.PackageID);

            return View("TourPackageDetails", model);
        }
        public ActionResult RequestSuccess(long id)
        {
            var obj = dbTour.GetTourPackageByID(id);
            ViewBag.PackageName = obj.PackageName;
            ViewBag.Duration = obj.TotalDays;
            return View();
        }
        private string MakeMailBody(long id)
        {
            StringBuilder mailbody = new StringBuilder();
            dalMstTourPackage dbTour = new dalMstTourPackage();
            MstTourPackageView pack = new MstTourPackageView();
            pack = dbTour.MstTourPackageView(id);
            dalMstTourPackageActivity dbActivity = new dalMstTourPackageActivity();
            IEnumerable<MstTourPackageActivityView> act = new List<MstTourPackageActivityView>();
            act = dbActivity.MstTourPackageActivityList(id);
            mailbody.Append("<h4>Package Details</h4>");
            mailbody.Append("Package Name: " + pack.PackageName + "<br/>Duration: " + pack.TotalDays + " Days / " + (pack.TotalDays - 1) + " Nights<br/>");
            mailbody.Append("Package Routing: " + pack.PackageRouting + "<br/>Package Description: " + pack.PackageRemarks);
            mailbody.Append("<h4>Tour Package Activities</h4>");
            mailbody.Append("<table style='width:700px;' border= '1'>");
            mailbody.Append("<tr>");
            mailbody.Append("<th style='border: 1px solid black'>Day List</th>");
            mailbody.Append("<th style='border: 1px solid black'>Activity</th>");
            mailbody.Append("<th style='border: 1px solid black'>Overnight Destination</th>");
            mailbody.Append("</tr>");
            foreach (var item in act)
            {
                mailbody.Append("<tr>");
                mailbody.Append("<td style='text-align:center;'>Day " + item.DayNo + "</td>");
                mailbody.Append("<td style='text-align:center;'>" + item.ActivityTitle + "</td>");
                mailbody.Append("<td style='text-align:center;'>" + item.DestinationName + "</td>");
                mailbody.Append("</tr>");
            }
            mailbody.Append("</table>");
            return mailbody.ToString();
        }
    }
}