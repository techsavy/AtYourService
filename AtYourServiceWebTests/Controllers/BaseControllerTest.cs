// -----------------------------------------------------------------------
// <copyright file="BaseControllerTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Tests.Controllers
{
    using System;
    using System.Web.Mvc;
    using Core.Commanding;
    using Domain.Users;
    using Models;
    using Moq;
    using NHibernate;
    using NUnit.Framework;
    using Web.Controllers;
    using Web.Util;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class BaseControllerTest
    {
        private const string UserName = "test";

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

            public void ExecuteExecuteCommand(ICommand command)
            {
                ExecuteCommand(command);
            }

            public TResult ExecuteExecuteCommand<TResult>(ICommand<TResult> command)
            {
                return ExecuteCommand(command);
            }

            public TResult ExecuteExecuteQuery<TResult>(Func<ISession, TResult> query)
            {
                return ExecuteQuery(query);
            }

            public UserInfo GetUserInfo()
            {
                return UserInfo;
            }
        }

        [Test]
        public void Unauthenticated_User_Action_Executing()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var actionExecutingContextMock = new Mock<ActionExecutingContext>();

            var controller = new FakeBaseController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Home/", UserName));

            controller.ExecuteOnActionExecuting(actionExecutingContextMock.Object);

            Assert.AreEqual(0, controller.Session.Count);
        }

        [Test]
        public void Authenticated_User_New_SessionAction_Executing()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, User>>()))
                .Returns(new Client {Email = "test@test.com", Name = UserName});

            var actionExecutingContextMock = new Mock<ActionExecutingContext>();

            var controller = new FakeBaseController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeAuthenticatedHttpContext("~/Home/", UserName));

            controller.ExecuteOnActionExecuting(actionExecutingContextMock.Object);

            Assert.AreEqual(1, controller.Session.Count);
            var userInfo = (UserInfo) controller.Session[SessionKeys.User];
            Assert.AreEqual(UserName, userInfo.Name);
        }

        [Test]
        public void Command_Executing()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);

            var controller = new FakeBaseController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Home/", UserName));

            var commandMock = new Mock<ICommand>();

            controller.ExecuteExecuteCommand(commandMock.Object);

            nHbernateContextMock.Verify(c => c.ExecuteCommand(It.IsAny<ICommand>()));

            const string result = "result";
            var commandResultMock = new Mock<ICommand<string>>();
            commandResultMock.Setup(c => c.Result).Returns(result);
            nHbernateContextMock.Setup(c => c.ExecuteCommand(It.IsAny<ICommand<string>>())).Returns(result);

            var actualResult = controller.ExecuteExecuteCommand(commandResultMock.Object);

            nHbernateContextMock.Verify(c => c.ExecuteCommand(It.IsAny<ICommand<string>>()));
            Assert.AreEqual(result, actualResult);
        }

        [Test]
        public void Query_Executing()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, NHibernateContext>>()))
                .Returns(nHbernateContextMock.Object);

            var controller = new FakeBaseController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Home/", UserName));

            var actualResult = controller.ExecuteExecuteQuery(s => s.QueryOver<NHibernateContext>().SingleOrDefault());

            nHbernateContextMock.Verify(c => c.ExecuteQuery(It.IsAny<Func<ISession, NHibernateContext>>()));
            Assert.AreEqual(nHbernateContextMock.Object, actualResult);
        }

        [Test]
        public void Unauthenticated_User_UserInfo()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);

            var controller = new FakeBaseController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Home/", UserName));

            var userInfo = controller.GetUserInfo();

            Assert.IsNull(userInfo);
        }

        [Test]
        public void Authenticated_User_UserInfo()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var userInfoMock = new Mock<UserInfo>();

            var controller = new FakeBaseController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeAuthenticatedHttpContext("~/Home/", UserName));
            controller.Session[SessionKeys.User] = userInfoMock.Object;

            var userInfo = controller.GetUserInfo();

            Assert.IsNotNull(userInfo);
            Assert.AreEqual(userInfoMock.Object, userInfo);
        }
    }
}
