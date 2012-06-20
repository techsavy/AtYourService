// -----------------------------------------------------------------------
// <copyright file="CreateClientCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using System;
    using Core.Geo;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CreateClientCommand : CreateUserCommand
    {
        public CreateClientCommand(string name, string email, string password, string brag, double latitude, double longitude, int source) 
            : base(name, email, password, brag, latitude, longitude)
        {
            Source = source;
        }

        public int Source { get; set; }

        protected override void OnExecute()
        {
            var client = new Client { Name = Name, Email = Email, Brag = Brag, IsVerified = false, LastActiveDate = DateTime.Now };
            client.SetPassword(Password);

            client.Location = PointFactory.Create(Latitude, Longitude);

            OnCreate(client);

            client.ClientSettings = new ClientSettings { AdCount = 0, Source = Source, Client = client };
            OnCreate(client.ClientSettings);

            Session.Save(client);
            Session.Save(client.ClientSettings);
        }
    }
}
