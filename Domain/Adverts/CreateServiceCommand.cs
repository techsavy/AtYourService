// -----------------------------------------------------------------------
// <copyright file="CreateServiceCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System;
    using System.IO;
    using Categories;
    using Core;
    using Core.Commanding;
    using Core.Geo;
    using Files;
    using Iesi.Collections.Generic;
    using Users;

    public class CreateServiceCommand : EntityCommand
    {
        public CreateServiceCommand(bool isServiceOffering, string title, string body, int categoryId,
            int clientId, double latitude, double longitude, DateTime? effectiveDate, IFileSystem fileSystem, FileBase imageFile)
        {
            IsServiceOffering = isServiceOffering;
            Title = title;
            Body = body;
            CategoryId = categoryId;
            ClientId = clientId;
            Latitude = latitude;
            Longitude = longitude;
            EffectiveDate = effectiveDate;
            ImageFile = imageFile;
            FileSystem = fileSystem;
        }

        public bool IsServiceOffering { get; private set; }

        public string Title { get; private set; }

        public string Body { get; private set; }

        public int CategoryId { get; private set; }

        public int ClientId { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public DateTime? EffectiveDate { get; private set; }

        public IFileSystem FileSystem { get; private set; }

        public FileBase ImageFile { get; private set; }

        protected override void OnExecute()
        {
            var client = Session.QueryOver<Client>().Fetch(c => c.ClientSettings).Eager.FutureValue();
            var category = new Category { Id = CategoryId };

            var service = IsServiceOffering ? (Service)new ServiceOffering() : new ServiceRequest();

            service.Title = Title;
            service.Body = Body;
            service.Client = client.Value;
            service.Category = category;
            service.EffectiveDate = EffectiveDate ?? DateTime.Now;
            service.Location = PointFactory.Create(Latitude, Longitude);

            service.ExpiryDate = DateTime.Now.Date.AddDays(90);

            ProcessImange(service);
            OnCreate(service);

            Session.Save(service);
        }

        private void ProcessImange(Service service)
        {
            if (ImageFile == null)
                return;

            var guidId = Guid.NewGuid();
            var extension = Path.GetExtension(ImageFile.FileName);
            var fileName = string.Format("img-{0}{1}", guidId, extension);

            FileSystem.Save(ImageFile.InputStream, fileName, true);

            var image = new Image { FileName = fileName, ContentType = ImageFile.ContentType };
            image.Services = new HashedSet<Service> { service };
            service.Image = image;
            
            OnCreate(image);
            Session.Save(image);
        }
    }
}
