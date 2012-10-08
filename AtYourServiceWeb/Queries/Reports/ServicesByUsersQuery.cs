
namespace AtYourService.Web.Queries.Reports
{
    using System.Collections.Generic;
    using Models;
    using NHibernate;
    using NHibernate.Transform;

    public class ServicesByUsersQuery : IQuery<IEnumerable<ServicesByUserViewModel>>
    {
        public IEnumerable<ServicesByUserViewModel> Execute(ISession session)
        {
            var query = @"SELECT u.Id, u.Name, u.Email, u.LastActiveDate, c.AdCount 
                        FROM Users u INNER JOIN ClientSettings c ON u.Id = c.UserId
                        ORDER BY c.AdCount DESC";

            return session.CreateSQLQuery(query)
                .SetResultTransformer(new AliasToBeanResultTransformer(typeof(ServicesByUserViewModel)))
                .List<ServicesByUserViewModel>();
        }
    }
}