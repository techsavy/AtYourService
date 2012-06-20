// -----------------------------------------------------------------------
// <copyright file="CommandWithReturnTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Tests
{
    using Commanding;
    using Moq;
    using NHibernate;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class CommandWithReturnTest
    {
        class FakeCommand : Command<string>
        {
            public FakeCommand(string returnValue)
            {
                ReturnValue = returnValue;
            }

            public bool OnExecuteCalled { get; private set; }

            protected override string OnExecute()
            {
                OnExecuteCalled = true;

                return ReturnValue;
            }

            public string ReturnValue { get; private set; }
        }

        private const string FakeReturnValue = "Fake";

        [Test]
        public void OnExecuteIsCalledWhenExecuting()
        {
            var context = CreateCommandContext();

            var command = new FakeCommand(FakeReturnValue);
            command.Execute(context);

            Assert.True(command.OnExecuteCalled);
        }

        [Test]
        public void ReturnsCorrectResult()
        {
            var context = CreateCommandContext();

            var command = new FakeCommand(FakeReturnValue);
            command.Execute(context);

            Assert.AreEqual(FakeReturnValue, command.Result);
        }

        private CommandContext CreateCommandContext()
        {
            var transaction = new Mock<ITransaction>();

            var session = new Mock<ISession>();
            session.Setup(s => s.BeginTransaction()).Returns(() => transaction.Object);
            var context = new CommandContext(session.Object, null);
            return context;
        }
    }
}
