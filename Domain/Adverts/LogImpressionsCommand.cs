// -----------------------------------------------------------------------
// <copyright file="LogImpressionsCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System;
    using System.Collections.Generic;
    using Core.Commanding;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogImpressionsCommand : Command
    {
        public LogImpressionsCommand(IList<int> serviceIds, string ip)
        {
            IP = ip;
            ServiceIds = serviceIds;
        }

        public IList<int> ServiceIds { get; private set; }
        public string IP { get; private set; }

        protected override void OnExecute()
        {
            var today = DateTime.Now;
            foreach (var serviceId in ServiceIds)
            {
                var impression = new Impression { ServiceId = serviceId, IP = this.IP, UserName = Context.User, Date = today };
                Session.Save(impression);
            }
        }
    }
}
