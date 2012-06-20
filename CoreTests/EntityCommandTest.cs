// -----------------------------------------------------------------------
// <copyright file="EntityCommandTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Tests
{
    using Commanding;
    using Data;
    using Moq;
    using NHibernate;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class EntityCommandTest
    {
        class FakeEntity : Entity
        {             
        }

        class FakeCreateCommand : EntityCommand
        {
            public FakeCreateCommand(FakeEntity fakeEntity)
            {
                FakeEntity = fakeEntity;
            }

            private FakeEntity FakeEntity { get; set; }

            protected override void OnExecute()
            {
                OnCreate(FakeEntity);
            }
        }

        private const string Username = "testUser";
        private const string InitialUsername = "Initial";

        [Test]
        public void OnCreateUpdatesEntityFields()
        {
            var transaction = new Mock<ITransaction>();

            var session = new Mock<ISession>();
            session.Setup(s => s.BeginTransaction()).Returns(() => transaction.Object);
            var context = new CommandContext(session.Object, Username);
            var entity = new FakeEntity();
            entity.LastModifiedBy = entity.CreatedBy = string.Empty;

            var command = new FakeCreateCommand(entity);
            command.Execute(context);

            Assert.AreEqual(Username, entity.CreatedBy);
            Assert.AreEqual(Username, entity.LastModifiedBy); 
        }

        class FakeUpdateCommand : EntityCommand
        {
            public FakeUpdateCommand(FakeEntity fakeEntity)
            {
                FakeEntity = fakeEntity;
            }

            private FakeEntity FakeEntity { get; set; }

            protected override void OnExecute()
            {
                OnUpdate(FakeEntity);
            }
        }

        [Test]
        public void OnUpdateUpdatesEntityFields()
        {
            var transaction = new Mock<ITransaction>();

            var session = new Mock<ISession>();
            session.Setup(s => s.BeginTransaction()).Returns(() => transaction.Object);
            var context = new CommandContext(session.Object, Username);
            var entity = new FakeEntity();
            entity.LastModifiedBy = entity.CreatedBy = InitialUsername;

            var command = new FakeUpdateCommand(entity);
            command.Execute(context);

            Assert.AreEqual(InitialUsername, entity.CreatedBy);
            Assert.AreEqual(Username, entity.LastModifiedBy);
        }
    }
}
