

namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Moderation;
    using Models;
    using Util;

    public class FlagsController : BaseController
    {
        public FlagsController(NHibernateContext nHibernateContext) : base(nHibernateContext)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult Create(FlagModel flagModel)
        {
            ExecuteCommand(new FlagServiceCommand(UserInfo.Id, flagModel.ServiceId, (FlagType)flagModel.Category, flagModel.Reason));

            return Json(new { Success = true });
        }
    }
}
