// -----------------------------------------------------------------------
// <copyright file="BaseControllerTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Moq;
    using NHibernate;
    using NUnit.Framework;
    using Web.Controllers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class BaseControllerTest
    {
        class FakeBaseController : BaseController
        {
            public FakeBaseController(ISession session) : base(session)
            {
            }

            public void ExecuteOnActionExecuting(ActionExecutingContext filterContext)
            {
                OnActionExecuting(filterContext);
            }
        }

        [Test]
        public void Unauthenticated_User_Action_Executing()
        {
            var sessionMock = new Mock<ISession>();
            var actionExecutingContextMock = new Mock<ActionExecutingContext>();

            var controller = new FakeBaseController(sessionMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Home/", "test"));

            controller.ExecuteOnActionExecuting(actionExecutingContextMock.Object);

            Assert.AreEqual(0, controller.Session.Count);
        }
    }
}
