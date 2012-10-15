// -----------------------------------------------------------------------
// <copyright file="PasswordResetRequest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using System;
    using Core.Data;

    public class PasswordResetRequest : Entity
    {
        public virtual string Token { get; set; }

        public virtual User User { get; set; }

        public virtual DateTime ExpiryDate { get; set; }
    }
}
