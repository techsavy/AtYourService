using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtYourService.Web.Controllers
{
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

    }
}
