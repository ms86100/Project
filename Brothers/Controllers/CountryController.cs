using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brothers.Entities.DataAccess;
using Brothers.Entities.Models;
using System.Text.RegularExpressions;


namespace Brothers.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class CountryController : Controller
    {
        public dalMstCountry db = new dalMstCountry();
        //
        // GET: /Country/
        public ActionResult List(int PageNo = 1, int PageSize = 10, string searchterm = null)
        {
            MstCountryViewModel obj = new MstCountryViewModel();
            int TotalRow;
            if (searchterm != null)
            {
                searchterm = searchterm.Trim();
                searchterm = Regex.Replace(searchterm, @"\s+", " ");
            }
            obj.MstCountryList = db.GetCountryPaged(PageNo, PageSize, out TotalRow,searchterm);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("pvCountryList", obj);
            }
            return View(obj);
        }
        public ActionResult Create()
        {
            return PartialView("pvCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(utblMstCountry country)
        {
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = db.Save(country);
                return Json(new { success = true });
            }
            return PartialView("pvCreate", country);
        }
        public ActionResult Edit(long id)
        {
            var model = db.GetCountryByID(id);
            return PartialView("pvEdit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(utblMstCountry country)
        {
            if (ModelState.IsValid)
            {
                TempData["ErrMsg"] = db.Save(country);
                // string url = Url.Action("list", "country");
                return Json(new { success = true });
            }
            return PartialView("pvEdit", country);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            TempData["ErrMsg"] = db.Delete(id);
            return RedirectToAction("list", "country");
        }
    }
}