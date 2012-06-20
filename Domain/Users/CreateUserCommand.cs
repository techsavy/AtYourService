// -----------------------------------------------------------------------
// <copyright file="CreateUserCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using Core.Commanding;
    using Core.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class CreateUserCommand : EntityCommand
    {
        protected CreateUserCommand(string name, string email, string password, string brag, double latitude, double longitude)
        {
            Name = name;
            Email = email;
            Password = password;
            Brag = brag;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string Brag { get; private set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        protected override void OnCreate(Entity entity)
        {
            base.OnCreate(entity);
            entity.LastModifiedBy = entity.CreatedBy = User.SystemUser;
        }
    }
}
