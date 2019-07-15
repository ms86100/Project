using Brothers.Entities.DataAccess;
using Brothers.Entities.Models;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Brothers.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class TourPackageController : Controller
    {
        dalMstTourPackage dbTour = new dalMstTourPackage();
        dalMstTourPackageActivity dbActivity = new dalMstTourPackageActivity();
        dalMstTourPackagePhoto dbPhoto = new dalMstTourPackagePhoto();
        dalTourPackageMap dbMap = new dalTourPackageMap();
        //
        // GET: /Destination/
        #region Tour Package
        public ActionResult List(int PageNo = 1, int PageSize = 10, string searchterm = null)
        {
            MstTourPackageViewModel obj = new MstTourPackageViewModel();
            int TotalRow;
            if (searchterm != null)
            {
                searchterm = searchterm.Trim();
                searchterm = Regex.Replace(searchterm, @"\s+", " ");
            }
            obj.MstTourList = dbTour.GetTourPackagePaged(PageNo, PageSize, out TotalRow, searchterm);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("pvTourPackageList", obj);
            }
            return View(obj);
        }
        public ActionResult Create()
        {
            MstTourPackageManageModel obj = new MstTourPackageManageModel();
            dalMstPackageType objPack = new dalMstPackageType();
            obj.MstPackageDropDown = objPack.MstPackageList;
            return View("Create", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstTourPackageManageModel tour)
        {
            dalMstPackageType objPack = new dalMstPackageType();
            if (ModelState.IsValid)
            {
                dbTour.Save(tour.MstTourPackage);
                return RedirectToAction("ManageTourActivities", new { id = tour.MstTourPackage.PackageID });
            }

            tour.MstPackageDropDown = objPack.MstPackageList;
            return View("Create", tour);
        }
        public ActionResult Edit(long id)
        {
            MstTourPackageManageModel obj = new MstTourPackageManageModel();
            dalMstPackageType objPack = new dalMstPackageType();
            obj.MstPackageDropDown = objPack.MstPackageList;
            obj.MstTourPackage = dbTour.GetTourPackageByID(id);
            return View("Edit", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstTourPackageManageModel data)
        {
            short totalDays = dbTour.GetTotalDays(data.MstTourPackage.PackageID);
            short newDays = data.MstTourPackage.TotalDays;
            if(newDays<totalDays)
            {
                TempData["ErrMsg"] = dbActivity.ManageDayLost(totalDays, newDays, data.MstTourPackage.PackageID);
                return RedirectToAction("Edit", data.MstTourPackage.PackageID);
            }
            if (ModelState.IsValid)
            {
                dbTour.Save(data.MstTourPackage);
                return RedirectToAction("ManageTourActivities", new { id = data.MstTourPackage.PackageID });
            }
            dalMstPackageType objPack = new dalMstPackageType();
            data.MstPackageDropDown = objPack.MstPackageList;
            return View("Edit", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            dbActivity.Delete(id);
            dbPhoto.DeleteByPackageID(id);
            TempData["ErrMsg"] = dbTour.Delete(id);
            return RedirectToAction("list");
        }
        #endregion Tour Package

        #region Tour Package Activities
        public ActionResult ManageTourActivities(long id)
        {
            MstTourPackageActivityModel obj = new MstTourPackageActivityModel();
            dalMstDestination objDest = new dalMstDestination();
            dalMstActivity act = new dalMstActivity();
            obj.MstActDropDownList = act.MstActivityList;
            obj.MstTourPackage = dbTour.GetTourPackageDetailsByID(id);
            obj.MstDestDropDownList = objDest.MstDestinationList;
            Dictionary<int, string> li = new Dictionary<int, string>();
            for (short j = dbActivity.GetDayNo(id); j < obj.MstTourPackage.TotalDays; j++)
            {
                li.Add((j + 1), "Day " + (j + 1));
            }
            SelectList list = new SelectList(li, "Key", "Value");
            ViewBag.Days = list;
            return View("ManageTourActivities", obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageTourActivities(MstTourPackageActivityModel touractivity)
        {
            touractivity.MstTourPackageActivity.PackageID = touractivity.MstTourPackage.PackageID;
            if (ModelState.IsValid)
            {
                short day = dbActivity.GetDayNo(touractivity.MstTourPackage.PackageID);
                day++;
                if(touractivity.MstTourPackageActivity.DayNo!=day)
                {
                    TempData["ErrMsg"] = 3;
                    return Json(new { success = true });
                }
                TempData["ErrMsg"] = dbActivity.Save(touractivity.MstTourPackageActivity);
                return Json(new { success = true });
            }
            dalMstDestination objDest = new dalMstDestination();
            dalMstActivity objAct = new dalMstActivity();
            //touractivity.MstTourPackage = dbTour.GetTourPackageDetailsByID(touractivity.MstTourPackage.PackageID);
            touractivity.MstDestDropDownList = objDest.MstDestinationList;
            touractivity.MstActDropDownList = objAct.MstActivityList;
            short j;
            Dictionary<int, string> li = new Dictionary<int, string>();
            for (j = dbActivity.GetDayNo(touractivity.MstTourPackage.PackageID); j < touractivity.MstTourPackage.TotalDays; j++)
            {
                li.Add((j + 1), "Day " + (j + 1));
            }
            SelectList list = new SelectList(li, "Key", "Value");
            ViewBag.Days = list;
            return View("ManageTourActivities", touractivity);
        }
        public ActionResult TourActivityList(long id, int PageNo = 1, int PageSize = 10)
        {
            MstTourPackageActivityModel obj = new MstTourPackageActivityModel();
            int TotalRow;
            obj.MstTourPackageActivityList = dbActivity.GetTourActivityPaged(PageNo, PageSize, out TotalRow, id);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            return PartialView("pvTourActivityList", obj);
        }
        public JsonResult GetActivityDetails(long id)
        {
            dalMstActivity obj = new dalMstActivity();
            return Json(obj.GetActivityByID(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditTourActivity(long id, long packid)
        {
            MstTourPackageActivityModel obj = new MstTourPackageActivityModel();
            dalMstDestination objDest = new dalMstDestination();
            dalMstActivity act = new dalMstActivity();
            obj.MstActDropDownList = act.MstActivityList;
            obj.MstTourPackage = dbTour.GetTourPackageDetailsByID(packid);
            obj.MstDestDropDownList = objDest.MstDestinationList;
            obj.MstTourPackageActivity = dbActivity.GetTourPackageActivityByID(id);
            Int16 dayNo = obj.MstTourPackageActivity.DayNo;
            Int16 totalday = obj.MstTourPackage.TotalDays;
            if(dayNo==totalday)
            {
                ViewBag.LastDay = "lastday";
            }
            return View("EditTourActivity", obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTourActivity(MstTourPackageActivityModel model)
        {
            model.MstTourPackageActivity.PackageID = model.MstTourPackage.PackageID;
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = dbActivity.Save(model.MstTourPackageActivity);
                return RedirectToAction("ManageTourActivities", new { id = model.MstTourPackage.PackageID });
            }
            dalMstDestination objDest = new dalMstDestination();
            dalMstActivity act = new dalMstActivity();
            model.MstActDropDownList = act.MstActivityList;
            model.MstDestDropDownList = objDest.MstDestinationList;
            return View("EditTourActivity", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTourActivity(long id, long packid)
        {
            TempData["ErrMsg"] = dbActivity.Delete(id);
            return RedirectToAction("TourActivityList", new { id = packid });
        }
        #endregion Tour Package Activities

        #region Tour Package Photos
        public ActionResult ManageTourPhotos(long id)
        {
            short totalDays = dbTour.GetTotalDays(id);
            short ActivityDays = dbActivity.GetTotalDayList(id);
            if (ActivityDays < totalDays)
            {
                TempData["ErrMsg"] = 5;
                return RedirectToAction("ManageTourActivities", new { id = id });
            }
            MstTourPackagePhotoModel obj = new MstTourPackagePhotoModel();
            obj.MstTourPackage = dbTour.GetTourPackageDetailsByID(id);

            return View("ManageTourPhotos", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageTourPhotos(MstTourPackagePhotoModel model)
        {
            model.MstTourPackagePhoto.PackageID = model.MstTourPackage.PackageID;
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = dbPhoto.Save(model.MstTourPackagePhoto);
                return Json(new { success = true });
            }
            model.MstTourPackage = dbTour.GetTourPackageDetailsByID(model.MstTourPackage.PackageID);
            return View("ManageTourPhotos", model);
        }
        public ActionResult TourPhotoList(long id, int PageNo = 1, int PageSize = 10)
        {
            MstTourPackagePhotoModel obj = new MstTourPackagePhotoModel();
            int TotalRow;
            obj.MstTourPackage = dbTour.GetTourPackageDetailsByID(id);
            obj.MstTourPackagePhotoList = dbPhoto.GetTourPackagePhotoPaged(PageNo, PageSize, out TotalRow, id);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            return PartialView("pvTourPhotoList", obj);
        }


        public ActionResult EditTourPhoto(long id, long packid)
        {
            MstTourPackagePhotoModel obj = new MstTourPackagePhotoModel();
            obj.MstTourPackage = dbTour.GetTourPackageDetailsByID(packid);
            obj.MstTourPackagePhoto = dbPhoto.GetPhotoDetailsByID(id);
            return View("EditTourPhoto", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTourPhoto(MstTourPackagePhotoModel model)
        {
            model.MstTourPackagePhoto.PackageID = model.MstTourPackage.PackageID;
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = dbPhoto.Save(model.MstTourPackagePhoto);
                return RedirectToAction("ManageTourPhotos", new { id = model.MstTourPackage.PackageID });
            }
            return View("EditTourPhoto", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTourPhoto(long id, long packid)
        {
            TempData["ErrMsg"] = dbPhoto.Delete(id);
            return RedirectToAction("TourPhotoList", new { id = packid });
        }
        public ActionResult MakeDefaultPhoto(long id, long packid)
        {
            dbPhoto.MakeDefaultPhoto(id, packid);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion Tour Package Photos

        #region Tour Package Map
        public ActionResult ManageTourMap(long id)
        {
            utblMstTourPackagePic pic = dbPhoto.GetDefaultPhoto(id);
            if (pic == null)
            {
                TempData["ErrMsg"] = 6;
                return RedirectToAction("ManageTourPhotos", new { id = id });
            }
            MstTourPackageMapModel obj = new MstTourPackageMapModel();
            obj.MstTourPackage = dbTour.GetTourPackageDetailsByID(id);
            obj.MstTourPackageMap = dbMap.GetTourMapByID(id);
            return View("ManageTourMap", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageTourMap(MstTourPackageMapModel model)
        {
            model.MstTourPackageMap.PackageID = model.MstTourPackage.PackageID;
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = dbMap.Save(model.MstTourPackageMap);
                return RedirectToAction("CompleteTourPackage", new { id = model.MstTourPackage.PackageID });
            }
            model.MstTourPackage = dbTour.GetTourPackageDetailsByID(model.MstTourPackage.PackageID);
            return View("ManageTourMap", model);
        }

        #endregion Tour Package Map
        public ActionResult CompleteTourPackage(long id)
        {
            List<MstTourPackageActivityView> obj = new List<MstTourPackageActivityView>();
            obj = dbActivity.MstTourPackageActivityList(id).ToList();
            List<string> tempDestination = new List<string>();
            foreach (var item in obj)
            {
                if (item.DestinationID != null)
                {
                    if (item.DestinationID == 5)
                    {
                        tempDestination.Add(item.OvernightDestination);
                    }
                    else
                    {
                        tempDestination.Add(item.DestinationName);
                    }
                }
            }
            string lastDestName = null;
            int flag = 1;
            int j = 0;
            List<string> destlist = new List<string>();
            for (int i = 0; i < tempDestination.Count; i++)
            {
                if(tempDestination[i].Equals(lastDestName))
                {
                    flag++;
                    j--;
                    destlist.RemoveAt(j);
                    destlist.Insert(j,tempDestination[i] + " (" + flag + " N)");
                }
                else
                {
                    flag = 1;
                    destlist.Insert(j,tempDestination[i] + " (" + flag + " N)");
                }
                j++;
                lastDestName = tempDestination[i];
            }
           
            string route = string.Join(" > ", destlist.ToArray());
            TempData["ErrMsg"] = dbTour.SavePackageRouting(id, route);
            return RedirectToAction("List");
        }
        public ActionResult TourPackageFullDetails(long id)
        {
            MstTourPackageFullDetailsViewModel obj = new MstTourPackageFullDetailsViewModel();
            int TotalRow;
            int PageNo = 1, PageSize = 10;
            obj.MstTourPackages = dbTour.MstTourPackageView(id);
            obj.MstTourActivityList = dbActivity.MstTourPackageActivityList(id);
            obj.MstTourPhotoList = dbPhoto.GetTourPackagePhotoPaged(PageNo, PageSize, out TotalRow, id);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if(obj.MstTourActivityList.Count()==0 || obj.MstTourPhotoList.Count()==0||obj.MstTourPackages==null)
            {
                TempData["ErrMsg"] = 8;
                return RedirectToAction("List");
            }
            return View("TourFullDetails", obj);
        }

    }
}