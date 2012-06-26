
namespace AtYourService.Web.Controllers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Web.Mvc;
    using AutoMapper;
    using Core.Commanding;
    using Models;
    using NHibernate;
    using Util;

    public abstract class BaseController : Controller
    {        
        protected BaseController(NHibernateContext nHibernateContext)
        {
            Contract.Requires(nHibernateContext != null);

            _nHibernateContext = nHibernateContext;
        }

        private readonly NHibernateContext _nHibernateContext;

        protected void ExecuteCommand(ICommand command)
        {
            Contract.Requires(command != null);

            _nHibernateContext.ExecuteCommand(command);
        }

        protected TResult ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            Contract.Requires(command != null);

            return _nHibernateContext.ExecuteCommand(command);
        }

        protected TResult ExecuteQuery<TResult>(Func<ISession, TResult> query)
        {
            Contract.Requires(query != null);

            return _nHibernateContext.ExecuteQuery(query);
        }

        protected UserInfo UserInfo { get { return (UserInfo)Session[SessionKeys.User]; } }

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
                var user = ExecuteQuery(
                    session => session.QueryOver<Domain.Users.User>()
                    .Where(u => u.Email == User.Identity.Name)
                    .SingleOrDefault());

                var userInfo = Mapper.Map<UserInfo>(user);
                Session[SessionKeys.User] = userInfo;
            }
        }
    }
}
