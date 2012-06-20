// -----------------------------------------------------------------------
// <copyright file="LoginUserCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


namespace AtYourService.Domain.Users
{
    using System;
    using Core.Commanding;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LoginUserCommand : Command<User>
    {
        public LoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }
        public string Password { get; private set; }

        protected override User OnExecute()
        {
            var user = Session.QueryOver<User>().And(u => u.Email == Email && u.IsVerified).SingleOrDefault();
            if (user != null && user.VerifyPassword(Password))
            {
                user.LastActiveDate = DateTime.Now;

                return user;
            }

            return null;
        }
    }
}
