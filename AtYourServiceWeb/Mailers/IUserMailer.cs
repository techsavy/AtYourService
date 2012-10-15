using Mvc.Mailer;

namespace AtYourService.Web.Mailers
{
    public interface IUserMailer
    {
        MvcMailMessage Welcome(string name, string verificationCode, string email);
        MvcMailMessage PasswordReset(string name, string token, string email);
    }
}