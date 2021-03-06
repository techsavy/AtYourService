﻿
namespace AtYourService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Domain;
    using Domain.Categories;
    using Helpers;
    using NHibernate.Transform;
    using Properties;
    using Security;
    using Util;

    public class CategoriesController : BaseController
    {

        //
        // GET: /Categories/

        public CategoriesController(NHibernateContext nHibernateContext)
            : base(nHibernateContext)
        {
        }

        public ActionResult Index()
        {
            return View(GetCategories());
        }

        [AdminOnly]
        public ActionResult Edit()
        {
            return View(GetCategories().OrderBy(category => category.Name));
        }

        private IList<Category> GetCategories()
        {
            var categories =  ExecuteQuery(
                session => session.QueryOver<Category>().Where(c => c.ParentCategory == null)
                .OrderBy(c => c.Name).Asc
                .Fetch(c => c.SubCategories).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List());

            return categories;
        }

        public ActionResult Menu()
        {
            var cats = ExecuteQuery(
                session => session.QueryOver<Category>().Where(c => c.ParentCategory == null)
                .Fetch(c => c.SubCategories).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Category>());

            return PartialView(cats);
        }

        [AdminOnly]
        public JsonResult AddSubCategory(string subCategoryName, int parentCategory)
        {
            ExecuteCommand(new AddCategoryCommand(parentCategory, subCategoryName));

            TempData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityCreateSuccess, "Sub category"));

            return Json(new {Success = true});
        }

        [AdminOnly]
        public JsonResult AddCategory(string mainCategoryName)
        {
            ExecuteCommand(new AddCategoryCommand(null, mainCategoryName));

            TempData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityCreateSuccess, "Category"));

            return Json(new { Success = true });
        }

        [AdminOnly]
        public JsonResult RenameCategory(int categoryId, string categoryName)
        {
            ExecuteCommand(new RenameCategoryCommand(categoryId, categoryName));

            TempData[ViewDataKeys.Message] = new SuccessMessage(Resources.CategoryRenameSuccess);

            return Json(new { Success = true });
        }

        [AdminOnly]
        public JsonResult DeleteCategory(int categoryId)
        {
            try
            {
                ExecuteCommand(new DeleteCategoryCommand(categoryId));

                TempData[ViewDataKeys.Message] = new SuccessMessage(Resources.CategoryRenameSuccess);

                return Json(new { Success = true });
            }
            catch (DomainException de)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(de.Message);

                return Json(new { Success = false, de.Message });
            }
        }
    }
}
