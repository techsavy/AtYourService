// -----------------------------------------------------------------------
// <copyright file="EditProfileCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using Core.Commanding;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EditProfileCommand : EntityCommand
    {
        public EditProfileCommand(int userId, string brag, double latitude, double longitude)
        {
            UserId = userId;
            Brag = brag;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int UserId { get; private set; }

        public string Brag { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        protected override void OnExecute()
        {
            var user = Session.Load<User>(UserId);

            user.Location.X = Longitude;
            user.Location.Y = Latitude;
            user.Brag = Brag;
        }
    }
}
