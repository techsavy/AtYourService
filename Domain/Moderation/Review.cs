// -----------------------------------------------------------------------
// <copyright file="Review.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Moderation
{
    using Adverts;
    using Core.Data;
    using Users;

    public class Review : Entity
    {
        public virtual Service Service { get; set; }

        public virtual Client Client { get; set; }

        public virtual byte Score { get; set; }

        public virtual string Body { get; set; }
    }
}
