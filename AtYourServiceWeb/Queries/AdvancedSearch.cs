

namespace AtYourService.Web.Queries
{
    using System;
    using System.Collections.Generic;
    using Domain.Adverts;
    using Models;
    using NetTopologySuite.Geometries;
    using NHibernate;

    public class AdvancedSearch : IQuery<IEnumerable<Service>>
    {
        public AdvancedSearch(SearchModel searchModel, Point userLocation, double distance)
        {
            _searchModel = searchModel;
            _userLocation = userLocation;
            _distance = distance;
        }

        private readonly SearchModel _searchModel;

        private readonly Point _userLocation;

        private readonly double _distance;

        private const int PageSize = 5;

        public IEnumerable<Service> Execute(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}