// -----------------------------------------------------------------------
// <copyright file="EmailVerification.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using Core.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EmailVerification : Entity
    {
        public virtual Client Client { get; set; }

        public virtual string VerificationCode { get; set; }
    }
}
