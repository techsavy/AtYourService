

namespace AtYourService.Web.Security
{
    using System.Web;
    using System.Web.Mvc;
    using Models;

    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = (UserInfo)httpContext.Session[SessionKeys.User];

            return user != null && user.IsAdmin;
        }
    }
}