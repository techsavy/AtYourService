
namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using NHibernate;

    public class HomeController : BaseController
    {
        public HomeController(ISession session) : base(session)
        {
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}
