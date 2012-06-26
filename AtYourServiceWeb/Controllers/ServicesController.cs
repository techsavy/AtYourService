

namespace AtYourService.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Domain.Adverts;
    using Domain.Categories;
    using Helpers;
    using Models;
    using NHibernate.Transform;
    using Util;

    public class ServicesController : BaseController
    {
        //
        // GET: /Services/

        public ServicesController(NHibernateContext nHibernateContext) : base(nHibernateContext)
        {
        }

        public ActionResult Create()
        {
            ViewData[ViewDataKeys.Categories] = GetCategories();

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateServiceModel createServiceModel)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateServiceCommand(true, createServiceModel.Title, createServiceModel.Body,
                                                       createServiceModel.CategoryId, UserInfo.Id, createServiceModel.Latitude,
                                                       createServiceModel.Longitude, null);
                ExecuteCommand(command);
            }

            ViewData[ViewDataKeys.Categories] = GetCategories();

            return View();
        }

        private IList<GroupedSelectListItem> GetCategories()
        {
            var categories = ExecuteQuery(
                session => session.QueryOver<Category>().Where(c => c.ParentCategory == null)
                .OrderBy(c => c.Name).Asc
                .Fetch(c => c.SubCategories).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List());

            var categorySelectList = categories.SelectMany(c => c.SubCategories).Select(
                    c => new GroupedSelectListItem
                        {
                            GroupKey = c.ParentCategory.Id.ToString(),
                            GroupName = c.ParentCategory.Name,
                            Text = c.Name,
                            Value = c.Id.ToString()
                        }).ToList();

            return categorySelectList;
        }
    }
}
