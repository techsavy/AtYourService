// -----------------------------------------------------------------------
// <copyright file="CreateReviewTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Tests.Moderation
{
    using Categories;
    using Domain.Adverts;
    using Domain.Moderation;
    using NUnit.Framework;
    using Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CreateReviewTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void Create_Review_With_All_Required_Information()
        {
            int serviceId, clientId;
            const byte score = 5;
            PupulateEntities(out clientId, out serviceId);

            const string body = "Review Body";
            var command = new CreateReviewCommand(serviceId, clientId, score, body);
            ExecuteCommand(command);

            var review = Session.QueryOver<Review>()
                .Fetch(r => r.Service).Eager
                .Where(r => r.Service.Id == serviceId && r.Client.Id == clientId)
                .And(r => r.Body == body).SingleOrDefault();

            Assert.NotNull(review);
            Assert.AreEqual(body, review.Body);
            Assert.AreEqual(score, review.Service.TotalReviewScore);
            Assert.AreEqual(1, review.Service.ReviewCount);
        }

        void PupulateEntities(out int clientId, out int serviceId)
        {
            var createCategory = new AddCategoryCommand(null, "test");
            ExecuteCommand(createCategory);

            var createClientCommand = new CreateClientCommand("test", "test@example.com", "123456", "brag", 23.565, 12.34, 1);
            ExecuteCommand(createClientCommand);

            var category = Session.QueryOver<Category>().FutureValue();
            var client = Session.QueryOver<Client>().FutureValue();

            var categoryId = category.Value.Id;
            clientId = client.Value.Id;

            var createServiceCommand = new CreateServiceCommand(true, "title", "body", categoryId, clientId, 23.234, 5.343, null, null, null);
            ExecuteCommand(createServiceCommand);

            var service =  Session.QueryOver<Service>().SingleOrDefault();

            serviceId = service.Id;
        }
    }
}
