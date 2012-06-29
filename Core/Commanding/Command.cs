// -----------------------------------------------------------------------
// <copyright file="Command.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Commanding
{
    using System;
    using System.Diagnostics.Contracts;
    using NHibernate;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class Command : ICommand
    {
        public ISession Session { get; private set; }

        public CommandContext Context { get; private set; }

        public void Execute(CommandContext context)
        {
            Contract.Requires(context != null, "context is null");
            Contract.Ensures(Context != null);

            Context = context;
            Session = context.Session;

            var transaction = Session.BeginTransaction();
            OnExecute();

            try
            {
                transaction.Commit();
            }
            catch (Exception exception)
            {
                OnException(exception);

                throw;
            }

            OnAfterExecute();
        }

        protected virtual void OnAfterExecute()
        {
        }

        protected abstract void OnExecute();

        protected virtual void OnException(Exception exception)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class Command<TResult> : ICommand<TResult>
    {
        public ISession Session { get; private set; }

        public CommandContext Context { get; private set; }

        public void Execute(CommandContext context)
        {
            Contract.Requires(context != null, "context is null");
            Contract.Ensures(Context != null);

            Context = context;
            Session = context.Session;

            var transaction = Session.BeginTransaction();
            Result = OnExecute();

            try
            {
                transaction.Commit();
            }
            catch (Exception exception)
            {
                OnException(exception);

                throw;
            }

            OnAfterExecute();
        }

        public TResult Result { get; private set; }

        protected abstract TResult OnExecute();

        protected virtual void OnAfterExecute()
        {
        }

        protected virtual void OnException(Exception exception)
        {
        }
    }
}
