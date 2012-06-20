// -----------------------------------------------------------------------
// <copyright file="BaseControllerTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Tests.Controllers
{
    using System;
    using System.Web.Mvc;
    using Domain.Users;
    using Moq;
    using NHibernate;
    using NUnit.Framework;
    using Util;
    using Web.Controllers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class BaseControllerTest
    {
        static BaseControllerTest()
        {
            AutoMapperConfiguration.Configure();
        }

        class FakeBaseController : BaseController
        {
            public FakeBaseController(NHibernateContext nHibernateContext)
                : base(nHibernateContext)
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
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object);
            var actionExecutingContextMock = new Mock<ActionExecutingContext>();

            var controller = new FakeBaseController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Home/", "test"));

            controller.ExecuteOnActionExecuting(actionExecutingContextMock.Object);

            Assert.AreEqual(0, controller.Session.Count);
        }

        [Test]
        public void Authenticated_User_New_SessionAction_Executing()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object);
            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, User>>()))
                .Returns(new Client {Email = "test@test.com", Name = "test"});

            var actionExecutingContextMock = new Mock<ActionExecutingContext>();

            var controller = new FakeBaseController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeAuthenticatedHttpContext("~/Home/", "test"));

            controller.ExecuteOnActionExecuting(actionExecutingContextMock.Object);

            Assert.AreEqual(1, controller.Session.Count);
        }
    }
}
