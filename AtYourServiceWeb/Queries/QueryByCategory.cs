

namespace AtYourService.Web.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Adverts;
    using Models;
    using Util;
    using NHibernate;
    using NHibernate.Spatial.Criterion.Lambda;
    using NetTopologySuite.Geometries;

    public class QueryByCategory : IQuery<IEnumerable<Service>>
    {
        public QueryByCategory(CategoryBrowseModel categoryBrowseModel, Point userLocation, double distance)
        {
            _categoryBrowseModel = categoryBrowseModel;
            _userLocation = userLocation;
            _distance = distance;
        }

        private readonly CategoryBrowseModel _categoryBrowseModel;

        private readonly Point _userLocation;

        private readonly double _distance;

        private const int PageSize = 5;

        public IEnumerable<Service> Execute(ISession session)
        {
            var sortField = _categoryBrowseModel.GetSortField();
            if (sortField == CategoryBrowseModel.Distance)
            {
                return QueryByDistance(session);
            }

            var page = _categoryBrowseModel.Page ?? 0;
            var orderProp = sortField == CategoryBrowseModel.Date ? "EffectiveDate" : "Views";

            return session.QueryOver<Service>()
                    .Where(s => s.EffectiveDate < DateTime.Now)
                    .WhereSpatialRestrictionOn(s => s.Location).IsWithinDistance(_userLocation, _distance)
                    .OrderBy(orderProp).Desc
                    .Fetch(s => s.Category).Eager
                    .JoinQueryOver(s => s.Category).Where(c => c.Id == _categoryBrowseModel.Id || c.ParentCategory.Id == _categoryBrowseModel.Id)
                    .Skip(page * PageSize).Take(PageSize)
                    .List();
        }

        private IEnumerable<Service> QueryByDistance(ISession session)
        {
            var services = session.QueryOver<Service>()
                    .Where(s => s.EffectiveDate < DateTime.Now)
                    .WhereSpatialRestrictionOn(s => s.Location).IsWithinDistance(_userLocation, _distance)
                    .Fetch(s => s.Category).Eager
                    .JoinQueryOver(s => s.Category).Where(c => c.Id == _categoryBrowseModel.Id || c.ParentCategory.Id == _categoryBrowseModel.Id)
                    .List();

            return services.OrderByDescending(s => s.Location.Coordinate.Distance(_userLocation.Coordinate)).ToList();
        }
    }
}