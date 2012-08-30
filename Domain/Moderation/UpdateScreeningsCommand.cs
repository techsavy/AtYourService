// -----------------------------------------------------------------------
// <copyright file="UpdateScreeningsCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Moderation
{
    using Adverts;
    using Core.Commanding;
    using NHibernate.Criterion;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UpdateScreeningsCommand : Command
    {
        public UpdateScreeningsCommand(int serviceId)
        {
            ServiceId = serviceId;
        }

        public int ServiceId { get; private set; }

        protected override void OnExecute()
        {            
            var totalViews = Session.QueryOver<View>().Where(i => i.ServiceId == ServiceId)
                .Select(Projections.RowCountInt64()).FutureValue<long>();
            var totalImpressions = Session.QueryOver<Impression>().Where(i => i.ServiceId == ServiceId)
                .Select(Projections.RowCountInt64()).FutureValue<long>();
            var service = Session.Load<Service>(ServiceId);

            service.Views = totalViews.Value;
            service.Impressions = totalImpressions.Value;
        }
    }
}
