

namespace AtYourService.Web.Queries.Reports
{
    using System.Collections.Generic;
    using Models;
    using NHibernate;
    using NHibernate.Transform;

    public class ServicesScreeningQuery : IQuery<IEnumerable<ServicesScreeningViewModel>>
    {
        private readonly int _clientId;

        public ServicesScreeningQuery(int clientId)
        {
            _clientId = clientId;
        }

        public IEnumerable<ServicesScreeningViewModel> Execute(ISession session)
        {
            var query =
                @"WITH ServiceViews(ServiceId, FirstDate, LatestDate, Count) AS(
                SELECT v.ServiceId, min(v.Date) AS FirstDate, max(v.Date) AS LatestDate, count(v.Date) AS Count FROM Service s 
                LEFT OUTER JOIN Screening v ON s.Id = v.ServiceId
                WHERE s.ClientId = :clientId AND v.ScreeningType = 'I'
                GROUP BY v.ServiceId)
                SELECT s.Id, s.Title, s.EffectiveDate, c.Name AS Category, v.FirstDate, v.LatestDate, v.Count FROM Service s 
                INNER JOIN ServiceViews v ON s.Id = v.ServiceId
                INNER JOIN Category c ON s.CategoryId = c.Id";

            return session.CreateSQLQuery(query)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ServicesScreeningViewModel)))
                .SetInt32("clientId", _clientId)
                .List<ServicesScreeningViewModel>();
        }
    }
}