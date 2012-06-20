
namespace AtYourService.Web.Util
{
    using System;
    using NHibernate;

    public class NHibernateContext
    {
        public NHibernateContext(ISession session)
        {
            Session = session;
        }

        public ISession Session { get; private set; }

        public virtual TResult ExecuteQuery<TResult>(Func<ISession, TResult> query)
        {
            return query(Session);
        }
    }
}