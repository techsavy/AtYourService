
namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Core.Commanding;
    using Models;
    using NHibernate;

    public abstract class BaseController : Controller
    {
        protected BaseController(ISession session)
        {
            NHibernateSession = session;
        }

        protected ISession NHibernateSession { get; private set; }

        protected void ExecuteCommand(ICommand command)
        {
            command.Execute(CreateCommandContext());
        }

        protected TResult ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            command.Execute(CreateCommandContext());

            return command.Result;
        }

        private CommandContext CreateCommandContext()
        {
            return new CommandContext(NHibernateSession, User.Identity.Name);
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetSessionVariables();
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// When user is authenticated from the persistant cookie we need to initialize the required session variables
        /// that would otherwise be set when user is logged in through login method
        /// </summary>
        private void SetSessionVariables()
        {
            if (User.Identity.IsAuthenticated && Session[SessionKeys.User] == null)
            {
                var user = NHibernateSession.QueryOver<Domain.Users.User>()
                    .Where(u => u.Email == User.Identity.Name)
                    .SingleOrDefault();

                var userInfo = Mapper.Map<UserInfo>(user);
                Session[SessionKeys.User] = userInfo;
            }
        }
    }
}
