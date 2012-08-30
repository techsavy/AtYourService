

namespace AtYourService.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Adverts;
    using Domain.Moderation;
    using Helpers;
    using Models;
    using Properties;
    using Util;

    public class ReviewsController : BaseController
    {
        //
        // GET: /Reviews/

        public ReviewsController(NHibernateContext nHibernateContext) : base(nHibernateContext)
        {
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public PartialViewResult Reviews(int serviceId)
        {
            var reviews = ExecuteQuery(
                session => session.QueryOver<Review>().Where(r => r.Service.Id == serviceId)
                .OrderBy(r => r.CreationDate).Desc
                .Fetch(r => r.Client).Eager
                .List());

            var reviewAllowed = Request.IsAuthenticated;

            if (reviewAllowed)
            {
                var previousReviews = ExecuteQuery(
                    session => session.QueryOver<Review>().Where(r => r.Service.Id == serviceId)
                    .And(r => r.Client.Id == UserInfo.Id)
                    .RowCount());

                reviewAllowed = previousReviews == 0;

                //if (reviewAllowed)
                //{
                //    var isServiceOwner = ExecuteQuery(
                //            session => session.QueryOver<Service>().Where(r => r.Id == serviceId)
                //            .And(r => r.Client.Id == UserInfo.Id)
                //            .RowCount());

                //    reviewAllowed = isServiceOwner == 0;
                //}
            }

            ViewData[ViewDataKeys.Reviews] = reviews;

            return PartialView(new ReviewsModel { ServiceId = serviceId, Page = 1, AllowCreate = reviewAllowed });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateReviewModel createReview)
        {
            if (ModelState.IsValid)
            {
                var createReviewCommand = new CreateReviewCommand(createReview.ServiceId, UserInfo.Id, createReview.Score, createReview.Body);
                ExecuteCommand(createReviewCommand);

                var updateScreeningsCommand = new UpdateScreeningsCommand(createReview.ServiceId);
                ExecuteNonBlockingCommand(updateScreeningsCommand);

                TempData[ViewDataKeys.Message] = new SuccessMessage(Resources.Reviewed); 
            }

            return RedirectToAction("Profile", "Accounts");
        }
    }
}
