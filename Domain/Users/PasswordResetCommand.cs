// -----------------------------------------------------------------------
// <copyright file="PasswordResetCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using System;
    using Core.Commanding;

    public class PasswordResetCommand : EntityCommand<bool>
    {
        public string Token { get; private set; }
        public string Password { get; private set; }

        public PasswordResetCommand(string token, string password)
        {
            Token = token;
            Password = password;
        }

        protected override bool OnExecute()
        {
            var now = DateTime.Now;
            var request = Session.QueryOver<PasswordResetRequest>()
                .Where(p => p.Token == Token)
                .And(p => p.ExpiryDate > now)
                .Fetch(p => p.User).Eager
                .SingleOrDefault();

            if (request == null || request.User == null)
            {
                return false;
            }

            request.User.ResetPassword(Password);
            OnUpdate(request.User);

            Session.Delete(request);

            return true;
        }
    }
}
