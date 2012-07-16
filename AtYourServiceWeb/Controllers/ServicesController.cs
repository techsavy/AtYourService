

namespace AtYourService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Core;
    using Core.Geo;
    using Domain.Adverts;
    using Domain.Categories;
    using Helpers;
    using Models;
    using NHibernate.Spatial.Criterion.Lambda;
    using NHibernate.Transform;
    using Queries;
    using Util;

    public class ServicesController : BaseController
    {
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Radius of the searched area
        /// </summary>
        private const double Distance = 10000; //10km
        //
        // GET: /Services/

        public ServicesController(NHibernateContext nHibernateContext, IFileSystem fileSystem) : base(nHibernateContext)
        {
            _fileSystem = fileSystem;
        }

        public ActionResult Category(CategoryBrowseModel categoryBrowseModel)
        {
            var services = ExecuteQuery(new QueryByCategory(categoryBrowseModel, GetUserLocation(), Distance));

            ViewData[ViewDataKeys.Services] = services;
            ViewData[ViewDataKeys.ServiceSerializationInfos] = Mapper.Map<IEnumerable<ServiceSerializeInfo>>(services);
            ViewData[ViewDataKeys.UserLocation] = GetUserLocation();

            return View(categoryBrowseModel);
        }

        public ActionResult Search(SearchModel searchModel)
        {
            var services = ExecuteQuery(new AdvancedSearch(searchModel, GetUserLocation(), Distance));

            ViewData[ViewDataKeys.Services] = services;
            ViewData[ViewDataKeys.ServiceSerializationInfos] = Mapper.Map<IEnumerable<ServiceSerializeInfo>>(services);
            ViewData[ViewDataKeys.UserLocation] = GetUserLocation();

            return View(searchModel);
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
                FileBase image = createServiceModel.Image != null ? new FileBaseAdapter(createServiceModel.Image) : null;

                var command = new CreateServiceCommand(true, createServiceModel.Title, createServiceModel.Body,
                                                       createServiceModel.CategoryId, UserInfo.Id, createServiceModel.Latitude,
                                                       createServiceModel.Longitude, null, _fileSystem, image);
                ExecuteCommand(command);
            }

            ViewData[ViewDataKeys.Categories] = GetCategories();

            return View();
        }

        [HttpPost]
        public JsonResult ServicesNearLocation(double lat, double lng)
        {
            var userLocation = PointFactory.Create(lat, lng);

            var services = ExecuteQuery(
                session => session.QueryOver<Service>()
                    .Where(s => s.EffectiveDate < DateTime.Now)
                    .WhereSpatialRestrictionOn(s => s.Location).IsWithinDistance(userLocation, Distance)
                    .Fetch(s => s.Category).Eager
                    .List());

            return Json(Mapper.Map<IEnumerable<ServiceSerializeInfo>>(services));
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
