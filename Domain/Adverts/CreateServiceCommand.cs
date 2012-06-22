// -----------------------------------------------------------------------
// <copyright file="CreateServiceCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System;
    using Categories;
    using Core.Commanding;
    using Core.Geo;
    using Users;

    public class CreateServiceCommand : EntityCommand
    {
        public CreateServiceCommand(bool isServiceOffering, string title, string body, int categoryId, 
            int clientId, double latitude, double longitude, DateTime? effectiveDate)
        {
            IsServiceOffering = isServiceOffering;
            Title = title;
            Body = body;
            CategoryId = categoryId;
            ClientId = clientId;
            Latitude = latitude;
            Longitude = longitude;
            EffectiveDate = effectiveDate;
        }

        public bool IsServiceOffering { get; private set; }

        public string Title { get; private set; }

        public string Body { get; private set; }

        public int CategoryId { get; private set; }

        public int ClientId { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public DateTime? EffectiveDate { get; private set; }

        protected override void OnExecute()
        {
            var client = Session.QueryOver<Client>().Fetch(c => c.ClientSettings).Eager.FutureValue();
            var category = new Category {Id = CategoryId};

            var service = IsServiceOffering ? (Service) new ServiceOffering() : new ServiceRequest();

            service.Title = Title;
            service.Body = Body;
            service.Client = client.Value;
            service.Category = category;
            service.EffectiveDate = EffectiveDate ?? DateTime.Now;
            service.Location = PointFactory.Create(Latitude, Longitude);

            service.ExpiryDate = DateTime.Now.Date.AddDays(90);

            OnCreate(service);

            Session.Save(service);
        }
    }
}
