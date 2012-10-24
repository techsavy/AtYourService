
namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using Util;

    public class HomeController : BaseController
    {
        public HomeController(NHibernateContext nHibernateContext)
            : base(nHibernateContext)
        {
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Formatting()
        {
            return View();
        }
    }
}
