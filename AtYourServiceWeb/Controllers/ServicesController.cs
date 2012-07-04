

namespace AtYourService.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Core;
    using Core.Geo;
    using Domain.Adverts;
    using Domain.Categories;
    using Helpers;
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Lucene.Net.Spatial.Tier;
    using Lucene.Net.Spatial.Tier.Projectors;
    using Models;
    using NHibernate.Spatial.Criterion.Lambda;
    using NHibernate.Transform;
    using NHibernate.Spatial;
    using Util;

    public class ServicesController : BaseController
    {
        private readonly IFileSystem _fileSystem;
        //
        // GET: /Services/

        public ServicesController(NHibernateContext nHibernateContext, IFileSystem fileSystem) : base(nHibernateContext)
        {
            _fileSystem = fileSystem;
        }

        public ActionResult Category(int id, int? page)
        {
            var skip = page ?? 0;

            var services = ExecuteQuery(
                session => session.QueryOver<Service>()
                    .Where(s => s.EffectiveDate < DateTime.Now)
                    .WhereSpatialRestrictionOn(s => s.Location).IsWithinDistance(PointFactory.Create(6.9319444, 79.8877778), 0.1)
                    .OrderBy(s => s.EffectiveDate).Desc
                    .Fetch(s => s.Category).Eager
                    .JoinQueryOver(s => s.Category).Where(c => c.Id == id || c.ParentCategory.Id == id)
                    .Skip(skip).Take(20)
                    .List());

            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Title", analyzer);
            var qr = queryParser.Parse("Title:t*");
            var dq = new DistanceQueryBuilder(6.9319444, 79.8877778, 100, "Location_Latitude", "Location_Longitude", CartesianTierPlotter.DefaltFieldPrefix, true);
            var dsort = new DistanceFieldComparatorSource(dq.DistanceFilter);
            var sort = new Sort(new SortField("geo_distance", dsort));

            var csq = new ConstantScoreQuery(dq.Filter);

            var q = ExecuteQuery(
                session => session.FullTextSession()
                               .CreateFullTextQuery(csq.Combine(new [] {qr}), typeof(Service)).List<Service>());

            return View(services);
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
