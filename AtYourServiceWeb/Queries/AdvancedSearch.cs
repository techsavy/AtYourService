// -----------------------------------------------------------------------
// <copyright file="AdvancedSearch.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Queries
{
    using System.Collections.Generic;
    using Domain.Adverts;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Lucene.Net.Spatial.Tier;
    using Lucene.Net.Spatial.Tier.Projectors;
    using Models;
    using NetTopologySuite.Geometries;
    using NHibernate;
    using Util;

    public class AdvancedSearch : IQuery<IEnumerable<Service>>
    {
        public AdvancedSearch(SearchModel searchModel, Point userLocation, double distance)
        {
            _searchModel = searchModel;
            _userLocation = userLocation;
            _distance = distance;
        }

        private const double MeterInMiles = 0.000621371;

        private readonly SearchModel _searchModel;

        private readonly Point _userLocation;

        private readonly double _distance;

        private const int PageSize = 5;

        public IEnumerable<Service> Execute(ISession session)
        {
            var serviceType = typeof (Service);
            var dq = new DistanceQueryBuilder(_userLocation.Y, _userLocation.X, _distance * MeterInMiles, "Location_Latitude", "Location_Longitude", CartesianTierPlotter.DefaltFieldPrefix, false);
            var metaTermQuery = new TermQuery(new Term("metafile", "doc"));

            var dsort = new DistanceFieldComparatorSource(dq.DistanceFilter);
            Sort sort = new Sort(new SortField("Location", dsort, false));

            var booleanQuery = new BooleanQuery();
            booleanQuery.Add(metaTermQuery, BooleanClause.Occur.SHOULD);
            
            if (!string.IsNullOrWhiteSpace(_searchModel.Terms))
            {
                var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                var queryParser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, new[] { "Title", "Body" }, analyzer);
                var termQuery = queryParser.Parse(_searchModel.Terms);

                booleanQuery.Add(termQuery, BooleanClause.Occur.MUST);
            }

            if (_searchModel.CategoryId != null)
            {
                var categoryQuery = new TermQuery(new Term("Category_Id", _searchModel.CategoryId.Value.ToString()));
                booleanQuery.Add(categoryQuery, BooleanClause.Occur.MUST);
            }

            if (_searchModel.Type != null)
            {
                if (_searchModel.Type.Value == (byte)ServiceType.Offering)
                {
                    serviceType = typeof (ServiceOffering);
                }
                else if (_searchModel.Type.Value == (byte)ServiceType.Request)
                {
                    serviceType = typeof(ServiceRequest);
                }
            }

            return session.FullTextSession().CreateFullTextQuery(booleanQuery, serviceType)
                .SetFilter(dq.Filter).SetFetchSize(PageSize).List<Service>();
        }
    }
}