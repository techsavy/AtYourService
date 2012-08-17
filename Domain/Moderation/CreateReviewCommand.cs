// -----------------------------------------------------------------------
// <copyright file="CreateReviewCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Moderation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Adverts;
    using Core.Commanding;
    using Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CreateReviewCommand : EntityCommand
    {
        public CreateReviewCommand(int serviceId, int clientId, byte score, string body)
        {
            ServiceId = serviceId;
            ClientId = clientId;
            Score = score;
            Body = body;
        }

        public int ServiceId { get; private set; }

        public int ClientId { get; private set; }

        public byte Score { get; private set; }

        public string Body { get; private set; }

        protected override void OnExecute()
        {
            var review = new Review
                             {
                                 Body = Body,
                                 Score = Score,
                                 Client = new Client { Id = ClientId },
                                 Service = new ServiceOffering { Id = ServiceId }
                             };
            OnCreate(review);
            Session.Save(review);
        }
    }
}
