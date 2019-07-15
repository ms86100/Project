using Brothers.Entities.DataAccess;
using Brothers.Entities.Models;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Brothers.Controllers
{
    public class PackageTypeController : Controller
    {
        public dalMstPackageType db = new dalMstPackageType();
        //
        // GET: /Country/
        public ActionResult List(int PageNo = 1, int PageSize = 10, string searchterm = null)
        {
            MstPackageViewModel obj = new MstPackageViewModel();
            int TotalRow;
            if (searchterm != null)
            {
                searchterm = searchterm.Trim();
                searchterm = Regex.Replace(searchterm, @"\s+", " ");
            }
            obj.MstPackageList = db.GetPackagePaged(PageNo, PageSize, out TotalRow, searchterm);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("pvPackageList", obj);
            }
            return View(obj);
        }
        public ActionResult Create()
        {
            return PartialView("pvCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(utblMstPackageType package)
        {
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = db.Save(package);
                return Json(new { success = true });
            }
            return PartialView("pvCreate", package);
        }
        public ActionResult Edit(long id)
        {
            var model = db.GetPackageByID(id);
            return PartialView("pvEdit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(utblMstPackageType package)
        {
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = db.Save(package);
                // string url = Url.Action("list", "country");
                return Json(new { success = true });
            }
            return PartialView("pvEdit", package);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            TempData["ErrMsg"] = db.Delete(id);
            return RedirectToAction("list", "packagetype");
        }
	}
}