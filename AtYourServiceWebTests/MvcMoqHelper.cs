
namespace AtYourService.Web.Tests
{
    using System;
    using System.Collections.Specialized;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Moq;
    using AtYourService.Web.Models;

    /// <summary>
    /// MVC Mocking helpers
    /// Taken from http://stackoverflow.com/a/690934/60108
    /// </summary>
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new MockHttpSession();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            return context.Object;
        }

        public static HttpContextBase FakeHttpContext(string url)
        {
            var context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);
            return context;
        }

        public static HttpContextBase FakeAuthenticatedHttpContext(string url, string userName)
        {
            var context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);

            var identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(true);
            identity.SetupGet(i => i.Name).Returns(userName);

            var principal = new Mock<IPrincipal>();
            principal.SetupGet(p => p.Identity).Returns(identity.Object);

            var contextMock = Mock.Get(context);
            contextMock.SetupGet(c => c.User).Returns(principal.Object);

            return context;
        }

        public static HttpContextBase FakeUnauthenticatedHttpContext(string url, string userName)
        {
            var context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);

            var identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(false);
            identity.SetupGet(i => i.Name).Returns(userName);

            var principal = new Mock<IPrincipal>();
            principal.SetupGet(p => p.Identity).Returns(identity.Object);

            var contextMock = Mock.Get(context);
            contextMock.SetupGet(c => c.User).Returns(principal.Object);

            return context;
        }

        public static void SetFakeControllerContext(this Controller controller)
        {
            var httpContext = FakeHttpContext();
            var context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            controller.ControllerContext = context;
        }

        public static void SetFakeControllerContext(this Controller controller, HttpContextBase httpContext)
        {
            var context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            controller.ControllerContext = context;
        }

        public static void SetUserInfo(this Controller controller)
        {
            controller.Session[SessionKeys.User] = new UserInfo();
        }

        static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
                return url.Substring(0, url.IndexOf("?", StringComparison.Ordinal));
            else
                return url;
        }

        static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                var parameters = new NameValueCollection();

                string[] parts = url.Split("?".ToCharArray());
                string[] keys = parts[1].Split("&".ToCharArray());

                foreach (string key in keys)
                {
                    string[] part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            else
            {
                return null;
            }
        }

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock.Get(request)
                .Setup(req => req.HttpMethod)
                .Returns(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (!url.StartsWith("~/"))
                throw new ArgumentException("Sorry, we expect a virtual url starting with \"~/\".");

            var mock = Mock.Get(request);

            mock.Setup(req => req.QueryString)
                .Returns(GetQueryStringParameters(url));
            mock.Setup(req => req.AppRelativeCurrentExecutionFilePath)
                .Returns(GetUrlFileName(url));
            mock.Setup(req => req.PathInfo)
                .Returns(string.Empty);
        }

        public static HtmlHelper CreateHtmlHelper(ViewDataDictionary vd)
        {
            var mockViewContext = new Mock<ViewContext>(
              new ControllerContext(
                new Mock<HttpContextBase>().Object,
                new RouteData(),
                new Mock<ControllerBase>().Object),
              new Mock<IView>().Object, vd,
              new TempDataDictionary(), new Mock<System.IO.TextWriter>().Object);

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(vd);

            return new HtmlHelper(mockViewContext.Object, mockViewDataContainer.Object);
        }

        public static HtmlHelper<TModel> CreateHtmlHelper<TModel>(ViewDataDictionary<TModel> vd)
        {
            var mockViewContext = new Mock<ViewContext>(
              new ControllerContext(
                new Mock<HttpContextBase>().Object,
                new RouteData(),
                new Mock<ControllerBase>().Object),
              new Mock<IView>().Object, vd,
              new TempDataDictionary(), new Mock<System.IO.TextWriter>().Object);

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(vd);

            return new HtmlHelper<TModel>(mockViewContext.Object, mockViewDataContainer.Object);
        }
    }
}
