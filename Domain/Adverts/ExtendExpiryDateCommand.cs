// -----------------------------------------------------------------------
// <copyright file="ExtendExpiryDateCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System;
    using Core.Commanding;

    public class ExtendExpiryDateCommand : EntityCommand
    {
        public ExtendExpiryDateCommand(int serviceId)
        {
            ServiceId = serviceId;
        }

        public int ServiceId { get; private set; }

        protected override void OnExecute()
        {
            var service = Session.Load<Service>(ServiceId);

            if (service == null || service.ExpiryDate > DateTime.Now)
                return;

            service.ExpiryDate = DateTime.Now.Date.AddDays(90);
            OnUpdate(service);
        }
    }
}
