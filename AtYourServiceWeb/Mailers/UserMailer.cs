using Mvc.Mailer;

namespace AtYourService.Web.Mailers
{
    public class UserMailer : MailerBase, IUserMailer
    {
        public UserMailer()
        {
            MasterName = "_Layout";
        }

        public virtual MvcMailMessage Welcome()
        {
            //ViewBag.Data = someObject;
            return Populate(x =>
            {
                x.Subject = "Welcome";
                x.ViewName = "Welcome";
                x.To.Add("erangak@gmail.com");
            });
        }
    }
}