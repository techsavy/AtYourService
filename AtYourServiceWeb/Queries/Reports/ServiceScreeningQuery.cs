

namespace AtYourService.Web.Queries.Reports
{
    using System;
    using System.Linq;
    using Domain.Adverts;
    using Models;
    using NHibernate;

    public class ServiceScreeningQuery : IQuery<ServiceScreeningViewModel>
    {
        private readonly int _serviceId;

        public ServiceScreeningQuery(int serviceId)
        {
            _serviceId = serviceId;
        }

        public ServiceScreeningViewModel Execute(ISession session)
        {
            var screenings = session.QueryOver<Screening>()
                .Where(s => s.ServiceId == _serviceId)
                .OrderBy(s => s.Date).Asc
                .List();

            var model = new ServiceScreeningViewModel();
            var impressions = screenings.OfType<Impression>().GroupBy(g => g.Date.Date)
                .Select(g => new {Date = g.Key, Count = g.Count() }).ToList();

            model.Impressions = new Series<DateTime, int> 
            { 
                Keys = impressions.Select(i => i.Date).ToList(), 
                Values = impressions.Select(i => i.Count).ToList()
            };

            var views = screenings.OfType<View>().GroupBy(g => g.Date.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() }).ToList();

            model.Views = new Series<DateTime, int>
            {
                Keys = views.Select(i => i.Date).ToList(),
                Values = views.Select(i => i.Count).ToList()
            };

            return model;
        }
    }
}