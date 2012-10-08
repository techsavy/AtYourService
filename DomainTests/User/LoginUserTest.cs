// -----------------------------------------------------------------------
// <copyright file="LoginUserTest.cs" company="">
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
    public class LoginUserTest : NHibernateIntegrationTestBase
    {
        [Test]
        public void LoginUser_With_Correct_Password_And_Unverified()
        {
            const string email = "test@gmail.com";

            var createCommand = new CreateClientCommand("test", email, "123456", "brag", 3.2342, 23.4545, 1);
            ExecuteCommand(createCommand);

            var loginUserCommand = new LoginUserCommand(email, "123456");
            ExecuteCommand(loginUserCommand);

            Assert.IsNull(loginUserCommand.Result);
        }

        [Test]
        public void LoginUser_With_Correct_Password_And_Verified()
        {
            const string email = "test@gmail.com";

            var createCommand = new CreateClientCommand("test", email, "123456", "brag", 3.2342, 23.4545, 1);
            ExecuteCommand(createCommand);

            var client = Session.QueryOver<Client>().Where(c => c.Email == email).SingleOrDefault();
            client.IsVerified = true;
            Session.Update(client);

            var loginUserCommand = new LoginUserCommand(email, "123456");
            ExecuteCommand(loginUserCommand);

            Assert.IsNotNull(loginUserCommand.Result);
        }

        [Test]
        public void LoginUser_With_Incorrect_Password_But_Verified()
        {
            const string email = "test@gmail.com";

            var createCommand = new CreateClientCommand("test", email, "123456", "brag", 3.2342, 23.4545, 1);
            ExecuteCommand(createCommand);

            var client = Session.QueryOver<Client>().Where(c => c.Email == email).SingleOrDefault();
            client.IsVerified = true;
            Session.Update(client);

            var loginUserCommand = new LoginUserCommand(email, "12345678");
            ExecuteCommand(loginUserCommand);

            Assert.AreEqual(1, loginUserCommand.Result.RetryCount);
        }
    }
}
