// -----------------------------------------------------------------------
// <copyright file="CreateAdvertTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Tests.Adverts
{
    using Categories;
    using Domain.Adverts;
    using NUnit.Framework;
    using Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CreateAdvertTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void Create_ServiceOffering_With_All_Required_Information()
        {
            var initialCount = Session.QueryOver<ServiceOffering>().RowCount();
            int categoryId, clientId;
            PupulateEntities(out categoryId, out clientId);

            var command = new CreateServiceCommand(true, "title", "body", categoryId, clientId, 23.234, 5.343, null, null, null);
            ExecuteCommand(command);

            var service = Session.QueryOver<ServiceOffering>()
                .Where(s => s.Category.Id == categoryId && s.Client.Id == clientId)
                .And(s => s.Title == "title" && s.Body == "body")
                .SingleOrDefault();

            Assert.NotNull(service);
            Assert.AreEqual(0, initialCount);
        }

        [Test]
        public void Create_ServiceRequest_With_All_Required_Information()
        {
            var initialCount = Session.QueryOver<ServiceOffering>().RowCount();
            int categoryId, clientId;
            PupulateEntities(out categoryId, out clientId);

            var command = new CreateServiceCommand(false, "title", "body", categoryId, clientId, 23.234, 5.343, null, null, null);
            ExecuteCommand(command);

            var service = Session.QueryOver<ServiceRequest>()
                .Where(s => s.Category.Id == categoryId && s.Client.Id == clientId)
                .And(s => s.Title == "title" && s.Body == "body")
                .SingleOrDefault();

            Assert.NotNull(service);
            Assert.AreEqual(0, initialCount);
        }

        void PupulateEntities(out int categoryId, out int clientId)
        {
            var createCategory = new AddCategoryCommand(null, "test");
            ExecuteCommand(createCategory);

            var createClientCommand = new CreateClientCommand("test", "test@example.com", "123456", "brag", 23.565, 12.34, 1);
            ExecuteCommand(createClientCommand);

            var category = Session.QueryOver<Category>().FutureValue();

            var client = Session.QueryOver<Client>().FutureValue();

            categoryId = category.Value.Id;
            clientId = client.Value.Id;
        }
    }
}
