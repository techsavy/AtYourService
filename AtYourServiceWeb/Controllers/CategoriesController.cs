
namespace AtYourService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Domain.Categories;
    using Helpers;
    using NHibernate;
    using NHibernate.Transform;
    using Properties;

    public class CategoriesController : BaseController
    {

        //
        // GET: /Categories/

        public CategoriesController(ISession session) : base(session)
        {
        }

        public ActionResult Index()
        {
            return View(GetCategories());
        }

        public ActionResult Edit()
        {
            return View(GetCategories().OrderBy(category => category.Name));
        }

        private IList<Category> GetCategories()
        {
            var categories = NHibernateSession.QueryOver<Category>().Where(c => c.ParentCategory == null)
                .OrderBy(c => c.Name).Asc
                .Fetch(c => c.SubCategories).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();

            return categories;
        }

        public ActionResult Menu()
        {
            var cats = NHibernateSession.QueryOver<Category>().Where(c => c.ParentCategory == null)
                .Fetch(c => c.SubCategories).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List<Category>();

            return PartialView(cats);
        }

        public JsonResult AddSubCategory(string subCategoryName, int parentCategory)
        {
            ExecuteCommand(new AddCategoryCommand(parentCategory, subCategoryName));

            TempData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityCreateSuccess, "Sub category"));

            return Json(new {Success = true});
        }

        public JsonResult AddCategory(string mainCategoryName)
        {
            ExecuteCommand(new AddCategoryCommand(null, mainCategoryName));

            TempData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityCreateSuccess, "Category"));

            return Json(new { Success = true });
        }

        public ActionResult RenameCategory(int categoryId, string categoryName)
        {
            ExecuteCommand(new RenameCategoryCommand(categoryId, categoryName));

            TempData[ViewDataKeys.Message] = new SuccessMessage(Resources.CategoryRenameSuccess);

            return Json(new { Success = true });
        }
    }
}
