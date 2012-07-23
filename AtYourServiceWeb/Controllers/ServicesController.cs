// -----------------------------------------------------------------------
// <copyright file="ServicesController.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

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
    using NetTopologySuite.Geometries;
    using Properties;
    using Queries;
    using Util;

    public class ServicesController : BaseController
    {
        private readonly IFileSystem _fileSystem;
        private readonly IGeoCodingService _geoCodingService;

        /// <summary>
        /// Radius of the searched area
        /// </summary>
        private const double Distance = 10000; //10km
        //
        // GET: /Services/

        public ServicesController(NHibernateContext nHibernateContext, IFileSystem fileSystem, IGeoCodingService geoCodingService) : base(nHibernateContext)
        {
            _fileSystem = fileSystem;
            _geoCodingService = geoCodingService;
        }

        public ActionResult Category(CategoryBrowseModel categoryBrowseModel)
        {
            var services = ExecuteQuery(new QueryByCategory(categoryBrowseModel, GetUserLocation(), Distance));

            LogImpressions(services);

            ViewData[ViewDataKeys.Services] = services;
            ViewData[ViewDataKeys.ServiceSerializationInfos] = Mapper.Map<IEnumerable<ServiceSerializeInfo>>(services);
            ViewData[ViewDataKeys.UserLocation] = GetUserLocation();

            return View(categoryBrowseModel);
        }

        public ActionResult Search(SearchModel searchModel)
        {
            Point location;
            if (string.IsNullOrWhiteSpace(searchModel.Location))
            {
                location = GetUserLocation();
            }
            else
            {
                location = _geoCodingService.GetCoordinates(searchModel.Location);
                if (location == null)
                {
                    TempData[ViewDataKeys.Message] = new FailMessage(string.Format(Resources.GeoCodeFail, searchModel.Location));
                }
            }
            
            if (location != null)
            {
                var services = ExecuteQuery(new AdvancedSearch(searchModel, location, Distance));

                LogImpressions(services);

                ViewData[ViewDataKeys.Services] = services;
                ViewData[ViewDataKeys.ServiceSerializationInfos] = Mapper.Map<IEnumerable<ServiceSerializeInfo>>(services);
                ViewData[ViewDataKeys.UserLocation] = location;
            }

            ViewData[ViewDataKeys.Categories] = GetCategories();
            ViewData[ViewDataKeys.ServiceTypes] = GetServiceTypes();
            ViewData[ViewDataKeys.SortTypes] = GetSortTypes();

            return View(searchModel);
        }

        public ActionResult Create()
        {
            ViewData[ViewDataKeys.Categories] = GetCategories();
            ViewData[ViewDataKeys.ServiceTypes] = GetServiceTypes();

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
            ViewData[ViewDataKeys.ServiceTypes] = GetServiceTypes();

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

            LogImpressions(services);

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

        private IEnumerable<SelectListItem> GetServiceTypes()
        {
            yield return new SelectListItem { Value = "1", Text = "Service"};
            yield return new SelectListItem { Value = "2", Text = "Wanted"};
        }

        private IEnumerable<SelectListItem> GetSortTypes()
        {
            yield return new SelectListItem { Value = "Distance", Text = "Distance" };
            yield return new SelectListItem { Value = "Date", Text = "Date" };
            yield return new SelectListItem { Value = "Views", Text = "Views" };
        }

        private void LogImpressions(IEnumerable<Service> services)
        {
            var serviceIds = services.Select(s => s.Id).ToList();
            var logImpressionsCommand = new LogImpressionsCommand(serviceIds, Request.UserHostAddress);

            ExecuteNonBlockingCommand(logImpressionsCommand);
        }
    }
}
