using Mvc.Mailer;

namespace AtYourService.Web.Mailers
{
    public interface IUserMailer
    {
        MvcMailMessage Welcome();
    }
}