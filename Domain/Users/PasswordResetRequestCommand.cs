// -----------------------------------------------------------------------
// <copyright file="PasswordResetRequestCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using System;
    using System.Security.Cryptography;
    using Core.Commanding;

    public class PasswordResetRequestCommand : EntityCommand<bool>
    {
        public string Email { get; private set; }

        public string Name { get; private set; }

        public string Token { get; private set; }

        public PasswordResetRequestCommand(string email)
        {
            Email = email;
        }

        protected override bool OnExecute()
        {
            var user = Session.QueryOver<User>()
                .Where(u => u.Email == Email)
                .SingleOrDefault();

            if (user == null)
            {
                return false;
            }

            Name = user.Name;

            var expiryDate = DateTime.Now.AddMonths(1);
            Token = GenerateToken();

            var request = new PasswordResetRequest { User = user, Token = Token, ExpiryDate = expiryDate };
            OnCreate(request);

            Session.Save(request);

            return true;
        }

        private string GenerateToken()
        {
            var rngCsp = new RNGCryptoServiceProvider();
            var randBytes = new byte[24];

            rngCsp.GetBytes(randBytes);

            return BitConverter.ToString(randBytes).Replace("-", string.Empty);
        }
    }
}
