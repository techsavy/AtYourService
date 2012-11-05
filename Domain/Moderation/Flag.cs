// -----------------------------------------------------------------------
// <copyright file="Flag.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Moderation
{
    using Adverts;
    using Core.Data;
    using Users;

    public enum FlagStatus { Pending, Accepted, Rejected }

    public enum FlagType { Spam, ViolatesTerms, ClaimOwnership, Other }

    public class Flag : Entity
    {
        public virtual Service Service { get; set; }

        public virtual Client Client { get; set; }

        public virtual FlagStatus Status { get; set; }

        public virtual FlagType Type { get; set; }

        public virtual string Message { get; set; }
    }
}
