// -----------------------------------------------------------------------
// <copyright file="ReportsController.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Adverts;
    using Queries.Reports;
    using Util;

    [Authorize]
    public class ReportsController : BaseController
    {
        //
        // GET: /Reports/

        public ReportsController(NHibernateContext nHibernateContext)
            : base(nHibernateContext)
        {
        }

        public ActionResult Screening(int serviceId)
        {
            var service = ExecuteQuery(session => session.QueryOver<Service>()
                                                .Where(s => s.Id == serviceId)
                                                .SingleOrDefault());

            if (!UserInfo.IsAdmin && service.Client.Id != UserInfo.Id)
            {
                RedirectToAction("Profile", "Accounts");
            }

            var model = ExecuteQuery(new ServiceScreeningQuery(serviceId));
            model.Service = service;

            return View(model);
        }

        public ActionResult Views()
        {
            var model = ExecuteQuery(new ServicesScreeningQuery(UserInfo.Id, true));

            return View(model);
        }

        public ActionResult Impressions()
        {
            var model = ExecuteQuery(new ServicesScreeningQuery(UserInfo.Id, false));

            return View(model);
        }

        public ActionResult CompetitorServices()
        {
            var model = ExecuteQuery(new CompetitorServicesQuery(UserInfo.Id));

            return View(model);
        }

    }
}
