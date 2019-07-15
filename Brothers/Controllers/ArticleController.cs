using Brothers.Entities.DataAccess;
using Brothers.Entities.Models;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brothers.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        BlogViewModel objViewModel = new BlogViewModel();
        dalBlog objDal = new dalBlog();
        dalMstEnquire ObjEnqDal = new dalMstEnquire();
        public ActionResult Post(string SearchTerm = "", int PageNo = 1, int PageSize = 4)
        {
            ViewBag.Current = "Blogs";
            ViewBag.SearchTerm = SearchTerm;
            int TotalRow;
            objViewModel.BlogList = objDal.BlogGetPaged(PageNo, PageSize, out TotalRow, SearchTerm);
            //int items = (PageSize % TotalRow);

            objViewModel.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = TotalRow };
            if (Request.IsAjaxRequest())
            {
                return PartialView("pvBlogList", objViewModel);
            }
            return View(objViewModel);

        }

        public ActionResult Recent(string BlogID)
        {
            ViewBag.Current = "Blogs";

            objViewModel.utblBlogs = objDal.GetBlogByID(BlogID);
            objViewModel.BlogPaging = objDal.GetBlogPrevNext(BlogID);
            return View(objViewModel);

        }

        public ActionResult Enquiry()
        {
            return PartialView("pvEnquiry");
        }

        [HttpPost]
        public ActionResult Enquiry(utblMstArticleEnquire ItemData)
        {
            if (string.IsNullOrEmpty(ItemData.Name) ||
                string.IsNullOrEmpty(ItemData.Email) ||
                string.IsNullOrEmpty(ItemData.Message)
                )
            {
                return Json("* Please fill all fields", JsonRequestBehavior.AllowGet);

            }
            TempData["ErrMsg"] = ObjEnqDal.ArticleEnquire(ItemData);
            return Json("Thanks for your submission, we will be in touch shortly ", JsonRequestBehavior.AllowGet);

        }

       
    }
}