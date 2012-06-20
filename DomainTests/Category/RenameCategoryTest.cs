// -----------------------------------------------------------------------
// <copyright file="RenameCategoryTest.cs" company="">
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
    public class RenameCategoryTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void Rename_Category()
        {
            string initialName = "test";
            ExecuteCommand(new AddCategoryCommand(null, initialName));

            var category = Session.QueryOver<Category>().Where(c => c.Name == initialName).SingleOrDefault();

            string newName = "newName";

            var renameCategoryCommand = new RenameCategoryCommand(category.Id, newName);
            ExecuteCommand(renameCategoryCommand);

            var renamedCategory = Session.QueryOver<Category>().Where(c => c.Id == category.Id).SingleOrDefault();
            
            Assert.AreEqual(newName, renamedCategory.Name);
        }
    }
}
