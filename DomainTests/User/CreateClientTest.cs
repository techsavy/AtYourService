// -----------------------------------------------------------------------
// <copyright file="CreateClientTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Tests.User
{
    using Tests;
    using Users;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>    
    public class CreateClientTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void Create_Client_With_All_Required_Information()
        {
            const string name = "test";
            var initialCount = Session.QueryOver<Client>().Where(c => c.Name == name).RowCount();

            double latitude = 3.2342;
            double longitude = 23.4545;

            var createCommand = new CreateClientCommand(name, "test@gmail.com", "123456", "brag", latitude, longitude, 1);
            ExecuteCommand(createCommand);

            var client = Session.QueryOver<Client>().Fetch(c => c.ClientSettings).Eager
                .Where(c => c.Name == name).SingleOrDefault();

            Assert.NotNull(client);
            Assert.AreEqual(name, client.Name);
            Assert.AreEqual(0, initialCount);

            Assert.AreEqual(latitude, client.Location.X);
            Assert.AreEqual(longitude, client.Location.Y);

            Assert.NotNull(client.ClientSettings);
            Assert.AreEqual(0, client.ClientSettings.AdCount);
        }
    }
}
