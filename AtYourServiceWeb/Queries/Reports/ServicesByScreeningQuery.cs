using System;


namespace AtYourService.Web.Queries.Reports
{
    using System.Collections.Generic;
    using Models;
    using NHibernate;
    using NHibernate.Transform;

    public class ServicesByScreeningQuery : IQuery<IEnumerable<ServicesScreeningViewModel>>
    {
        private readonly bool _views;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public ServicesByScreeningQuery(bool views, DateTime startDate, DateTime endDate)
        {
            _views = views;
            _startDate = startDate;
            _endDate = endDate;
        }

        public IEnumerable<ServicesScreeningViewModel> Execute(ISession session)
        {
            var query = @"WITH ServiceViews(ServiceId, FirstDate, LatestDate, Count) AS(
                SELECT v.ServiceId, min(v.Date) AS FirstDate, max(v.Date) AS LatestDate, count(v.Date) AS Count FROM Service s 
                INNER JOIN Screening v ON s.Id = v.ServiceId
                WHERE v.ScreeningType = :screeningType AND v.Date > :startDate AND v.Date < :endDate
                GROUP BY v.ServiceId)
                SELECT s.Id, s.Title, s.EffectiveDate, c.Name AS Category, v.FirstDate, v.LatestDate, v.Count FROM Service s 
                INNER JOIN ServiceViews v ON s.Id = v.ServiceId
                INNER JOIN Category c ON s.CategoryId = c.Id
                ORDER BY v.Count desc";

            return session.CreateSQLQuery(query)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ServicesScreeningViewModel)))
                .SetDateTime("startDate", _startDate)
                .SetDateTime("endDate", _endDate)
                .SetString("screeningType", _views ? "V" : "I")
                .List<ServicesScreeningViewModel>();
        }
    }
}