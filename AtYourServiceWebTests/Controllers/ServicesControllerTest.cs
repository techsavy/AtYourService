// -----------------------------------------------------------------------
// <copyright file="ServicesControllerTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Commanding;
    using Core.Geo;
    using Domain.Adverts;
    using Domain.Categories;
    using Iesi.Collections.Generic;
    using Models;
    using Moq;
    using NHibernate;
    using NUnit.Framework;
    using Queries;
    using Web.Controllers;
    using Web.Helpers;
    using Web.Util;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class ServicesControllerTest
    {
        static ServicesControllerTest()
        {
            AutoMapperConfiguration.Configure();
        }

        private const string UserName = "test";

        [Test]
        public void Create_Get()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var categories = GetSampleCategories();

            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, IList<Category>>>()))
                .Returns(categories);

            var fileSystemMock = new Mock<IFileSystem>();
            var geoCodingServicemMock = new Mock<IGeoCodingService>();

            var controller = new ServicesController(nHbernateContextMock.Object, fileSystemMock.Object, geoCodingServicemMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Services/Create/", UserName));

            controller.Create();

            var actualCategories = (IList<GroupedSelectListItem>)controller.ViewData[ViewDataKeys.Categories];

            Assert.AreEqual(3, actualCategories.Count);
        }

        [Test]
        public void Create_Post_valid()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var createServiceModel = new CreateServiceModel();
            var categories = GetSampleCategories();

            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, IList<Category>>>()))
                .Returns(categories);

            var fileSystemMock = new Mock<IFileSystem>();
            var geoCodingServicemMock = new Mock<IGeoCodingService>();

            var controller = new ServicesController(nHbernateContextMock.Object, fileSystemMock.Object, geoCodingServicemMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeAuthenticatedHttpContext("~/Services/Create/", UserName));
            controller.SetUserInfo();

            controller.Create(createServiceModel);

            nHbernateContextMock.Verify(c => c.ExecuteCommand(It.IsAny<ICommand>()));
        }

        [Test]
        public void Category_Browse()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var categoryBrowseModel = new CategoryBrowseModel();
            var services = GetSampleServices();

            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<QueryByCategory>()))
                .Returns(services);

            var fileSystemMock = new Mock<IFileSystem>();
            var geoCodingServicemMock = new Mock<IGeoCodingService>();

            var controller = new ServicesController(nHbernateContextMock.Object, fileSystemMock.Object, geoCodingServicemMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeAuthenticatedHttpContext("~/Services/Category/1/", UserName));
            controller.SetUserInfo();

            controller.Category(categoryBrowseModel);

            Assert.AreEqual(services, controller.ViewData[ViewDataKeys.Services]);
        }

        [Test]
        public void ServicesNearLocation()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var services = GetSampleServices();

            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, IList<Service>>>()))
                .Returns(services);

            var fileSystemMock = new Mock<IFileSystem>();
            var geoCodingServicemMock = new Mock<IGeoCodingService>();

            var controller = new ServicesController(nHbernateContextMock.Object, fileSystemMock.Object, geoCodingServicemMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeAuthenticatedHttpContext("~/Services/ServicesNearLocation", UserName));
            controller.SetUserInfo();

            var jsonResult = controller.ServicesNearLocation(1.337, 3.37);

            dynamic json = jsonResult.Data;
            Assert.AreEqual(2, json.Count);
        }

        private List<Category> GetSampleCategories()
        {
            var foo = new Category { Id = 1, Name = "foo" };
            var fooSub1 = new Category { Id = 2, Name = "foo sub 1", ParentCategory = foo };
            var fooSub2 = new Category { Id = 3, Name = "foo sub 2", ParentCategory = foo };
            foo.SubCategories = new HashedSet<Category> { fooSub1, fooSub2 };

            var bar = new Category { Id = 4, Name = "bar" };
            var barSub = new Category { Id = 5, Name = "bar sub", ParentCategory = bar };

            bar.SubCategories = new HashedSet<Category> { barSub };

            return new List<Category> { foo, bar };
        }

        private List<Service> GetSampleServices()
        {
            var foo = new ServiceOffering { Id = 1, Title = "foo", Body = "foo", Category = new Category { Id = 1, Name = "foo"}};

            var bar = new ServiceOffering { Id = 1, Title = "bar", Body = "bar", Category = new Category { Id = 2, Name = "bar" } };

            return new List<Service> { foo, bar };
        }
    }
}
