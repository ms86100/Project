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
    public class DestinationController : Controller
    {
        dalMstDestination db = new dalMstDestination();
        //
        // GET: /Destination/
        public ActionResult List(int PageNo = 1, int PageSize = 10, string searchterm = null)
        {
            MstDestinationViewModel obj = new MstDestinationViewModel();
            int TotalRow;
            if (searchterm != null)
            {
                searchterm = searchterm.Trim();
                searchterm = Regex.Replace(searchterm, @"\s+", " ");
            }
            obj.MstDestinationList = db.GetDestinationPaged(PageNo, PageSize, out TotalRow, searchterm);
            obj.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("pvDestinationList", obj);
            }
            return View(obj);
        }
        public ActionResult Create()
        {
            MstDestinationManageModel obj = new MstDestinationManageModel();
            dalMstCountry objCountry = new dalMstCountry();
            obj.MstCountryDropDown = objCountry.MstCountryList;
            return PartialView("pvCreate", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstDestinationManageModel destination)
        {
            dalMstCountry objCountry = new dalMstCountry();
            if(ModelState.IsValid)
            {
                TempData["ErrMsg"] = db.Save(destination.utblMstDestinations);
                return Json(new { success = true });
            }
            destination.MstCountryDropDown = objCountry.MstCountryList;
            return PartialView("pvCreate", destination);
        }
        public ActionResult Edit(long id)
        {
            MstDestinationManageModel obj = new MstDestinationManageModel();
            dalMstCountry objCountry = new dalMstCountry();
            obj.MstCountryDropDown = objCountry.MstCountryList;
            obj.utblMstDestinations = db.GetDestinationByID(id);
            return PartialView("pvEdit", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstDestinationManageModel data)
        {
            if(ModelState.IsValid)
            {
                TempData["ErrMsg"] = db.Save(data.utblMstDestinations);
                return Json(new { success = true });
            }
            dalMstCountry objCountry = new dalMstCountry();
            data.MstCountryDropDown = objCountry.MstCountryList;
            return PartialView("pvEdit", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            TempData["ErrMsg"] = db.Delete(id);
            return RedirectToAction("list", "destination");
        }
    }
}