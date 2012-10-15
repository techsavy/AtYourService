
namespace AtYourService.Web.Mailers
{
    using Mvc.Mailer;

    public class UserMailer : MailerBase, IUserMailer
    {
        public UserMailer()
        {
            MasterName = "_Layout";
        }

        public virtual MvcMailMessage Welcome(string name, string verificationCode, string email)
        {
            ViewBag.Username = name;
            ViewBag.VerificationCode = verificationCode;
            ViewBag.Email = email;

            return Populate(x =>
            {
                x.Subject = "Welcome";
                x.ViewName = "Welcome";
                x.To.Add(email);
            });
        }

        public virtual MvcMailMessage PasswordReset(string name, string token, string email)
        {
            ViewBag.Username = name;
            ViewBag.Token = token;
            ViewBag.Email = email;

            return Populate(x =>
            {
                x.Subject = "Password Reset";
                x.ViewName = "PasswordReset";
                x.To.Add(email);
            });
        }
    }
}