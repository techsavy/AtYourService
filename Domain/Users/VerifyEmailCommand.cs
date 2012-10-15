// -----------------------------------------------------------------------
// <copyright file="VerifyEmailCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using Core.Commanding;

    public class VerifyEmailCommand : EntityCommand<bool>
    {
        public string Email { get; private set; }

        public string VerificationCode { get; private set; }

        public VerifyEmailCommand(string email, string verificationCode)
        {
            Email = email;
            VerificationCode = verificationCode;
        }

        protected override bool OnExecute()
        {
            var verification = Session.QueryOver<EmailVerification>()
                .Where(e => e.VerificationCode == VerificationCode)
                .And(e => e.IsVerified == false)
                .Fetch(e => e.Client).Eager.SingleOrDefault();

            if (verification == null || verification.Client.Email != Email)
            {
                return false;
            }

            verification.IsVerified = true;
            OnUpdate(verification);

            return true;
        }
    }
}
