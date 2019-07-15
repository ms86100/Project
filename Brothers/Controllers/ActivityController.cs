using Brothers.Entities.DataAccess;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Brothers.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ActivityController : Controller
    {
        dalMstActivity db = new dalMstActivity();
        //
        // GET: /Destination/
        public ActionResult List(int PageNo = 1, int PageSize = 10, string searchterm = null)
        {
            MstActivityViewModel obj = new MstActivityViewModel();
            int TotalRow;
            if (searchterm != null)
            {
                searchterm = searchterm.Trim();
                searchterm = Regex.Replace(searchterm, @"\s+", " ");
            }
            obj.MstActivityList = db.GetActivityPaged(PageNo, PageSize, out TotalRow, searchterm);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("pvActivityList", obj);
            }
            return View(obj);
        }
        public ActionResult Create()
        {
            MstActivityManageModel obj = new MstActivityManageModel();
            dalMstDestination objDest = new dalMstDestination();
            obj.MstDestDropDown = objDest.MstAllDestinations;
            return PartialView("pvCreate", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstActivityManageModel activity)
        {
            dalMstDestination objDest = new dalMstDestination();
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = db.Save(activity.MstActivities);
                return Json(new { success = true });
            }
            activity.MstDestDropDown = objDest.MstAllDestinations;
            return PartialView("pvCreate", activity);
        }
        public ActionResult Edit(long id)
        {
            MstActivityManageModel obj = new MstActivityManageModel();
            dalMstDestination objDest = new dalMstDestination();
            obj.MstDestDropDown = objDest.MstAllDestinations;
            obj.MstActivities = db.GetActivityByID(id);
            return PartialView("pvEdit", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstActivityManageModel data)
        {
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = db.Save(data.MstActivities);
                return Json(new { success = true });
            }
            dalMstDestination objDest = new dalMstDestination();
            data.MstDestDropDown = objDest.MstAllDestinations;
            return PartialView("pvEdit", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            TempData["ErrMsg"] = db.Delete(id);
            return RedirectToAction("list", "activity");
        }
	}
}