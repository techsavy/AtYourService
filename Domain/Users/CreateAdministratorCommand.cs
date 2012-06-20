// -----------------------------------------------------------------------
// <copyright file="CreateUserCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using System;

    public class CreateAdministratorCommand : CreateUserCommand
    {
        public CreateAdministratorCommand(string name, string email, string password, string brag, double latitude, double longitude) 
            : base(name, email, password, brag, latitude, longitude)
        {
        }

        protected override void OnExecute()
        {
            var admin = new Administrator { Name = Name, Email = Email, Brag = Brag, IsVerified = false, LastActiveDate = DateTime.Now };
            admin.SetPassword(Password);
            OnCreate(admin);
            Session.Save(admin);
        }
    }
}