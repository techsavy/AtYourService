
namespace AtYourService.Web.Util
{
    using System;
    using System.Diagnostics.Contracts;
    using Core.Commanding;
    using NHibernate;
    using Queries;

    /// <summary>
    /// NHibernate based commanding and querying context
    /// </summary>
    public class NHibernateContext
    {
        public NHibernateContext(ISession session, string userName)
        {
            UserName = userName;
            Session = session;
        }

        public string UserName { get; private set; }

        public ISession Session { get; private set; }

        /// <summary>
        /// Executes nHibernate query
        /// </summary>
        /// <typeparam name="TResult">Return type of query</typeparam>
        /// <param name="query">nHibernate query</param>
        /// <returns></returns>
        public virtual TResult ExecuteQuery<TResult>(Func<ISession, TResult> query)
        {
            Contract.Requires(query != null);

            return query(Session);
        }

        public virtual TResult ExecuteQuery<TResult>(IQuery<TResult> query)
        {
            Contract.Requires(query != null);

            return query.Execute(Session);
        }

        private CommandContext CreateCommandContext()
        {
            return new CommandContext(Session, UserName);
        }

        /// <summary>
        /// Executes a command
        /// </summary>
        /// <param name="command"></param>
        public virtual void ExecuteCommand(ICommand command)
        {
            Contract.Requires(command != null);

            command.Execute(CreateCommandContext());
        }

        /// <summary>
        /// Executes a command and returns its result
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual TResult ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            Contract.Requires(command != null);

            command.Execute(CreateCommandContext());

            return command.Result;
        }
    }
}