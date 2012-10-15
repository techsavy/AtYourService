// -----------------------------------------------------------------------
// <copyright file="CreateClientCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using System;
    using System.Security.Cryptography;
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

        public string EmailVerificationCode { get; private set; }

        protected override void OnExecute()
        {
            var client = new Client { Name = Name, Email = Email, Brag = Brag, IsVerified = false, LastActiveDate = DateTime.Now };
            client.SetPassword(Password);

            client.Location = PointFactory.Create(Latitude, Longitude);

            client.EmailVerification = new EmailVerification { VerificationCode =  GenerateVerificationCode(), Client = client };

            OnCreate(client);
            OnCreate(client.EmailVerification);

            client.ClientSettings = new ClientSettings { AdCount = 0, Source = Source, Client = client };
            OnCreate(client.ClientSettings);

            Session.Save(client);
            Session.Save(client.ClientSettings);
            Session.Save(client.EmailVerification);

            EmailVerificationCode = client.EmailVerification.VerificationCode;
        }

        private string GenerateVerificationCode()
        {
            var rngCsp = new RNGCryptoServiceProvider();
            var randBytes = new byte[24];

            rngCsp.GetBytes(randBytes);

            return BitConverter.ToString(randBytes).Replace("-", string.Empty);
        }
    }
}
