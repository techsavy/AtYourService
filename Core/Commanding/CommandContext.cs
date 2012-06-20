// -----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Commanding
{
    using NHibernate;

    public class CommandContext
    {
        public CommandContext(ISession session, string user)
        {
            Session = session;
            User = user;
        }

        public ISession Session { get; private set; }

        public string User { get; private set; }
    }
}
