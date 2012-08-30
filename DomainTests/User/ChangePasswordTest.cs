// -----------------------------------------------------------------------
// <copyright file="ChangePasswordTest.cs" company="">
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
    public class ChangePasswordTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void Change_Password_With_Correct_Existing_Password()
        {
            var createClientCommand = new CreateClientCommand("test", "test@example.com", "123456", "brag", 23.565, 12.34, 1);
            ExecuteCommand(createClientCommand);

            var client = Session.QueryOver<Client>().FutureValue();

            var changePasswordCommand = new ChangePasswordCommand(client.Value.Id, "123456", "456789");
            ExecuteCommand(changePasswordCommand);

            Assert.IsTrue(changePasswordCommand.Result);
        }

        [Test]
        public void Change_Password_With_Incorrect_Existing_Password()
        {
            var createClientCommand = new CreateClientCommand("test", "test@example.com", "123456", "brag", 23.565, 12.34, 1);
            ExecuteCommand(createClientCommand);

            var client = Session.QueryOver<Client>().FutureValue();

            var changePasswordCommand = new ChangePasswordCommand(client.Value.Id, "987654", "456789");
            ExecuteCommand(changePasswordCommand);

            Assert.IsFalse(changePasswordCommand.Result);
        }
    }
}
