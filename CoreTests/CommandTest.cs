// -----------------------------------------------------------------------
// <copyright file="CommandTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Tests
{
    using System;
    using Commanding;
    using Moq;
    using NHibernate;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class CommandTest
    {
        class FakeCommand : Command
        {
            public bool OnExecuteCalled { get; private set; }

            protected override void OnExecute()
            {
                OnExecuteCalled = true;
            }

            public bool OnExceptionCalled { get; private set; }

            protected override void OnException(Exception exception)
            {
                base.OnException(exception);

                OnExceptionCalled = true;
            }
        }
        
        [Test]
        public void OnExecute_Is_Called_When_Executing()
        {
            var transaction = new Mock<ITransaction>();

            var session = new Mock<ISession>();
            session.Setup(s => s.BeginTransaction()).Returns(() => transaction.Object);
            var context = new CommandContext(session.Object, null);

            var command = new FakeCommand();
            command.Execute(context);

            Assert.True(command.OnExecuteCalled);
        }

        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void OnException_Is_Called_When_Commit_Throws_Exception()
        {
            var transaction = new Mock<ITransaction>();
            transaction.Setup(t => t.Commit()).Throws<NHibernate.Exceptions.GenericADOException>();

            var session = new Mock<ISession>();
            session.Setup(s => s.BeginTransaction()).Returns(() => transaction.Object);
            var context = new CommandContext(session.Object, null);

            var command = new FakeCommand();
            command.Execute(context);

            Assert.True(command.OnExecuteCalled);
            Assert.True(command.OnExceptionCalled);
        }
    }
}
