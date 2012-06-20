// -----------------------------------------------------------------------
// <copyright file="AddCategoryTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Tests.Category
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Categories;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AddCategoryTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void Add_Top_Level_Category()
        {
            const string categoryName = "test";
            var initialCount = Session.QueryOver<Category>().Where(c => c.Name == categoryName).RowCount();

            var addCategoryCommand = new AddCategoryCommand(null, categoryName);
            ExecuteCommand(addCategoryCommand);

            var category = Session.QueryOver<Category>().Where(c => c.Name == categoryName).SingleOrDefault();

            Assert.AreEqual(0, initialCount);
            Assert.AreEqual(categoryName, category.Name);
            Assert.IsNull(category.ParentCategory);
        }

        [Test]
        public void Add_Sub_Category()
        {
            string parentCategoryName = "parent";
            var addParentCategoryCommand = new AddCategoryCommand(null, parentCategoryName);
            ExecuteCommand(addParentCategoryCommand);

            var parentCategory = Session.QueryOver<Category>().Where(c => c.Name == parentCategoryName).SingleOrDefault();

            const string categoryName = "test";
            var addCategoryCommand = new AddCategoryCommand(parentCategory.Id, categoryName);
            ExecuteCommand(addCategoryCommand);

            var category = Session.QueryOver<Category>().Where(c => c.Name == categoryName).SingleOrDefault();

            Assert.AreEqual(categoryName, category.Name);
            Assert.IsNotNull(category.ParentCategory);
        }
    }
}
