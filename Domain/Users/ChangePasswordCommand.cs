// -----------------------------------------------------------------------
// <copyright file="ChangePasswordCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using Core.Commanding;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ChangePasswordCommand : Command<bool>
    {
        public ChangePasswordCommand(int userId, string oldPassword, string newPassword)
        {
            UserId = userId;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public int UserId { get; private set; }

        public string OldPassword { get; private set; }

        public string NewPassword { get; private set; }

        protected override bool OnExecute()
        {
            var user = Session.Load<User>(UserId);

            return user.ChangePassword(OldPassword, NewPassword);
        }
    }
}
