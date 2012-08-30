// -----------------------------------------------------------------------
// <copyright file="CreateReviewCommand.cs" company="">
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
            var service = Session.Load<Service>(ServiceId);

            service.ReviewCount++;
            service.TotalReviewScore += Score;

            var review = new Review
                             {
                                 Body = Body,
                                 Score = Score,
                                 Client = new Client { Id = ClientId },
                                 Service = service
                             };

            OnCreate(review);
            Session.Save(review);
        }
    }
}
