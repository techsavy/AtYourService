// -----------------------------------------------------------------------
// <copyright file="DeleteCategoryTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Tests.Category
{
    using Categories;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeleteCategoryTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void Delete_SubCategory_Without_Any_Services()
        {
            const string topCategoryName = "Top Category";
            ExecuteCommand(new AddCategoryCommand(null, topCategoryName));
            var topCat = Session.QueryOver<Category>().Where(c => c.Name == topCategoryName)
                .SingleOrDefault();

            const string subCategoryName = "Sub Category";
            ExecuteCommand(new AddCategoryCommand(topCat.Id, subCategoryName));
            var subCat = Session.QueryOver<Category>().Where(c => c.Name == subCategoryName)
                .SingleOrDefault();

            ExecuteCommand(new DeleteCategoryCommand(subCat.Id));

            var count = Session.QueryOver<Category>().Where(c => c.Name == subCategoryName).RowCount();
            Assert.AreEqual(0, count);
        }

        [Test]
        [ExpectedException(typeof(DomainException))]
        public void Delete_SubCategory_With_Services()
        {
            const string topCategoryName = "Top Category";
            ExecuteCommand(new AddCategoryCommand(null, topCategoryName));
            var topCat = Session.QueryOver<Category>().Where(c => c.Name == topCategoryName)
                .SingleOrDefault();

            const string subCategoryName = "Sub Category";
            ExecuteCommand(new AddCategoryCommand(topCat.Id, subCategoryName));
            var subCat = Session.QueryOver<Category>().Where(c => c.Name == subCategoryName)
                .SingleOrDefault();

            subCat.AdCount = 5;

            Session.Update(subCat);
            ExecuteCommand(new DeleteCategoryCommand(subCat.Id));
        }

        [Test]
        public void Delete_TopCategory_Without_Any_Services()
        {
            const string topCategoryName = "Top Category";
            ExecuteCommand(new AddCategoryCommand(null, topCategoryName));
            var topCat = Session.QueryOver<Category>().Where(c => c.Name == topCategoryName)
                .SingleOrDefault();

            ExecuteCommand(new DeleteCategoryCommand(topCat.Id));

            var count = Session.QueryOver<Category>().Where(c => c.Name == topCategoryName).RowCount();
            Assert.AreEqual(0, count);
        }

        [Test]
        [ExpectedException(typeof(DomainException))]
        public void Delete_TopCategory_With_Services()
        {
            const string topCategoryName = "Top Category";
            ExecuteCommand(new AddCategoryCommand(null, topCategoryName));
            var topCat = Session.QueryOver<Category>().Where(c => c.Name == topCategoryName)
                .SingleOrDefault();

            topCat.AdCount = 5;

            Session.Update(topCat);
            ExecuteCommand(new DeleteCategoryCommand(topCat.Id));
        }

        [Test]
        [ExpectedException(typeof(DomainException))]
        public void Delete_TopCategory_With_SubCategories()
        {
            const string topCategoryName = "Top Category";
            ExecuteCommand(new AddCategoryCommand(null, topCategoryName));
            var topCat = Session.QueryOver<Category>().Where(c => c.Name == topCategoryName)
                .SingleOrDefault();

            const string subCategoryName = "Sub Category";
            ExecuteCommand(new AddCategoryCommand(topCat.Id, subCategoryName));

            ExecuteCommand(new DeleteCategoryCommand(topCat.Id));
        }

        [Test]
        [ExpectedException(typeof(DomainException))]
        public void Delete_Invalid_Category()
        {
            ExecuteCommand(new DeleteCategoryCommand(int.MaxValue));
        }
    }
}
