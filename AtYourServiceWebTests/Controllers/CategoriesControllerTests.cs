// -----------------------------------------------------------------------
// <copyright file="CategoriesControllerTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using Core.Commanding;
    using Domain.Categories;
    using Moq;
    using NHibernate;
    using NUnit.Framework;
    using Util;
    using Web.Controllers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class CategoriesControllerTests
    {
        private const string UserName = "test";

        [Test]
        public void Index()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var categories = new List<Category>();

            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, IList<Category>>>()))
                .Returns(categories);

            var controller = new CategoriesController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Categories/", UserName));

            controller.Index();

            Assert.AreEqual(categories, controller.ViewData.Model);
        }

        [Test]
        public void Edit()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var categories = new List<Category>();

            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, IList<Category>>>()))
                .Returns(categories);

            var controller = new CategoriesController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Categories/Edit", UserName));

            controller.Edit();

            Assert.AreEqual(categories, controller.ViewData.Model);
        }

        [Test]
        public void Menu()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);
            var categories = new List<Category>();

            nHbernateContextMock.Setup(c => c.ExecuteQuery(It.IsAny<Func<ISession, IList<Category>>>()))
                .Returns(categories);

            var controller = new CategoriesController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Categories/Menu", UserName));

            controller.Menu();

            Assert.AreEqual(categories, controller.ViewData.Model);
        }

        [Test]
        public void AddCategory()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);

            var controller = new CategoriesController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Categories/AddCategory", UserName));

            var jsonResult = controller.AddCategory("subCat");

            nHbernateContextMock.Verify(c => c.ExecuteCommand(It.IsAny<ICommand>()));

            dynamic json = jsonResult.Data;
            Assert.IsTrue(json.Success);
        }

        [Test]
        public void AddSubCategory()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);

            var controller = new CategoriesController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Categories/AddSubCategory", UserName));

            var jsonResult = controller.AddSubCategory("subCat", 4);

            nHbernateContextMock.Verify(c => c.ExecuteCommand(It.IsAny<ICommand>()));

            dynamic json = jsonResult.Data;
            Assert.IsTrue(json.Success);
        }

        [Test]
        public void RenameCategory()
        {
            var sessionMock = new Mock<ISession>();
            var nHbernateContextMock = new Mock<NHibernateContext>(sessionMock.Object, UserName);

            var controller = new CategoriesController(nHbernateContextMock.Object);
            controller.SetFakeControllerContext(MvcMockHelpers.FakeUnauthenticatedHttpContext("~/Categories/RenameCategory", UserName));

            var jsonResult = controller.RenameCategory(4, "newName");

            nHbernateContextMock.Verify(c => c.ExecuteCommand(It.IsAny<ICommand>()));

            dynamic json = jsonResult.Data;
            Assert.IsTrue(json.Success);
        }
    }
}
