// -----------------------------------------------------------------------
// <copyright file="NHibernateIntegrationTestBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Tests
{
    using System;
    using System.Transactions;
    using Core.Commanding;
    using Data;
    using NHibernate;
    using NHibernate.Cfg;
    using NUnit.Framework;
    using Environment = NHibernate.Cfg.Environment;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class NHibernateIntegrationTestBase : IDisposable
    {
        private TransactionScope Scope;
        private static Configuration Configuration;
        private static ISessionFactory SessionFactory;

        protected ISession Session;
        protected string User { get; private set; }

        public NHibernateIntegrationTestBase() : this("TestUser")
        {            
        }

        public NHibernateIntegrationTestBase(string user)
        {
            User = user;

            if (Configuration == null)
            {
                Configuration = new NHibernateConfigurator().Configure()
                    .SetProperty(Environment.ConnectionString, "Data Source=localhost;Initial Catalog=AtYourServiceDomainTests;Integrated Security=True;");

                SessionFactory = Configuration.BuildSessionFactory();
            }
        }

        protected CommandContext CreateCommandContext()
        {
            return new CommandContext(Session, User);
        }

        protected void ExecuteCommand(ICommand command)
        {
            command.Execute(CreateCommandContext());
        }

        protected TResult ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            command.Execute(CreateCommandContext());

            return command.Result;
        }

        [SetUp]
        public void Init()
        {
            Scope = new TransactionScope();
            Session = SessionFactory.OpenSession();
        }

        [TearDown]
        public void Cleanup()
        {
            Scope.Dispose();
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
