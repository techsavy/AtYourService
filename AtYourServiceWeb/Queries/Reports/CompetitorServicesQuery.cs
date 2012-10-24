
namespace AtYourService.Web.Queries.Reports
{
    using System.Collections.Generic;
    using Models;
    using NHibernate;
    using NHibernate.Transform;

    public class CompetitorServicesQuery : IQuery<IEnumerable<CompetitorServiceViewModel>>
    {
        private readonly int _clientId;

        public CompetitorServicesQuery(int clientId)
        {
            _clientId = clientId;
        }

        public IEnumerable<CompetitorServiceViewModel> Execute(ISession session)
        {
            var query = @"SELECT others.Id as OtherServiceId, others.Title as OtherServiceTitle, ownServices.Id as OwnServiceId, " +
                "ownServices.Title as OwnServiceTitle, others.Location.STDistance(ownServices.Location) as Distance, c.Name as Category " +
                "FROM Service ownServices " +
                "INNER JOIN Service others ON others.CategoryId = ownServices.CategoryId AND others.Location.STDistance(ownServices.Location) < 10000 " +
                "INNER JOIN Category c ON ownServices.CategoryId = c.id " +
                "WHERE ownServices.ClientId = :client AND others.ClientId != :client " +
                "AND ownServices.EffectiveDate < getdate() AND others.EffectiveDate < getdate() " +
                "AND ownServices.ExpiryDate > getdate() AND others.ExpiryDate > getdate() " +
                "AND ownServices.IsDeleted = 0 AND others.IsDeleted = 0 " +
                "ORDER BY ownServices.Title";

            return session.CreateSQLQuery(query).SetInt32("client", _clientId)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CompetitorServiceViewModel)))
                .List<CompetitorServiceViewModel>();
        }
    }
}