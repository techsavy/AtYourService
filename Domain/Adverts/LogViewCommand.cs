// -----------------------------------------------------------------------
// <copyright file="LogViewCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System;
    using Core.Commanding;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogViewCommand : Command
    {
        public LogViewCommand(int serviceId, string ip)
        {
            IP = ip;
            ServiceId = serviceId;
        }

        public int ServiceId { get; private set; }

        public string IP { get; private set; }

        protected override void OnExecute()
        {
            var view = new View { ServiceId = ServiceId, IP = IP, UserName = Context.User, Date = DateTime.Now };
            Session.Save(view);
        }
    }
}
