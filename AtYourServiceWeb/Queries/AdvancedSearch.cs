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
            var dq = new DistanceQueryBuilder(_userLocation.Y, _userLocation.X, _distance * MeterInMiles, "Location_Latitude", "Location_Longitude", CartesianTierPlotter.DefaltFieldPrefix, false);
            var metaTermQuery = new TermQuery(new Term("metafile", "doc"));

            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            var queryParser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, new [] { "Title", "Body" }, analyzer);
            var termQuery = queryParser.Parse(_searchModel.Terms);

            var dsort = new DistanceFieldComparatorSource(dq.DistanceFilter);
            Sort sort = new Sort(new SortField("Location", dsort, false));

            var booleanQuery = new BooleanQuery();
            booleanQuery.Add(metaTermQuery, BooleanClause.Occur.SHOULD);
            booleanQuery.Add(termQuery, BooleanClause.Occur.MUST);

            return session.FullTextSession().CreateFullTextQuery(booleanQuery, typeof(Service))
                .SetFilter(dq.Filter).SetFetchSize(PageSize).List<Service>();
        }
    }
}