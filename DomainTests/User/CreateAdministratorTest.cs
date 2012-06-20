// -----------------------------------------------------------------------
// <copyright file="CreateAdministratorTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Tests.User
{
    using NUnit.Framework;
    using Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CreateAdministratorTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void CreateAdministrator_With_All_Required_Information()
        {
            const string name = "test";
            var initialCount = Session.QueryOver<Administrator>().Where(c => c.Name == name).RowCount();

            double latitude = 3.2342;
            double longitude = 23.4545;

            var createCommand = new CreateAdministratorCommand(name, "test@gmail.com", "123456", "brag", latitude, longitude);
            ExecuteCommand(createCommand);

            var administrator = Session.QueryOver<Administrator>()
                .Where(c => c.Name == name).SingleOrDefault();

            Assert.NotNull(administrator);
            Assert.AreEqual(name, administrator.Name);
            Assert.AreEqual(0, initialCount);            
        }
    }
}
