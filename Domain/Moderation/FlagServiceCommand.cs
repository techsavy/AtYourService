// -----------------------------------------------------------------------
// <copyright file="FlagServiceCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Moderation
{
    using Adverts;
    using Core.Commanding;
    using Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FlagServiceCommand : EntityCommand
    {
        public int ClientId { get; private set; }

        public int ServiceId { get; private set; }

        public FlagType FlagType { get; private set; }

        public string Message { get; private set; }

        public FlagServiceCommand(int clientId, int serviceId, FlagType flagType, string message)
        {
            Message = message;
            ClientId = clientId;
            ServiceId = serviceId;
            FlagType = flagType;
        }

        protected override void OnExecute()
        {
            var service = Session.Load<Service>(ServiceId);
            var client = Session.Load<Client>(ClientId);

            var flag = new Flag { Client = client, Service = service, Message = Message, Type = FlagType, Status = FlagStatus.Pending };

            OnCreate(flag);
            Session.Save(flag);
        }
    }
}
