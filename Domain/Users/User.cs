// -----------------------------------------------------------------------
// <copyright file="User.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Users
{
    using System;
    using System.Diagnostics.Contracts;
    using Core.Data;
    using Core.Security;
    using NetTopologySuite.Geometries;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class User : Entity
    {
        public const int MaxRetryCount = 5;

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Brag { get; set; }

        public virtual string PasswordHash { get; protected set; }

        public virtual short RetryCount { get; set; }

        public virtual bool IsLocked { get { return RetryCount < MaxRetryCount; } }

        public virtual string ProfilePicId { get; set; }

        public virtual bool IsVerified { get; set; }

        public virtual DateTime LastActiveDate { get; set; }

        public virtual DateTime? ExpiryDate { get; set; }

        public virtual Point Location { get; set; }
        
        /// <summary>
        /// Sets the password of new user.
        /// </summary>
        /// <param name="plainTextPassword">Password text</param>
        public virtual void SetPassword(string plainTextPassword)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(plainTextPassword));

            if (!IsTransient())
                throw new InvalidOperationException("Use \"ChangePassword\" method to change password of an existing user.");

            var crypto = GetCryptoProvider();
            PasswordHash = crypto.HashPassword(plainTextPassword);
        }

        /// <summary>
        /// Changes password of an existing user.
        /// </summary>
        /// <param name="oldPassword">Existing password</param>
        /// <param name="newPassword">New Password</param>
        /// <returns>True if password change was successful.</returns>
        public virtual bool ChangePassword(string oldPassword, string newPassword)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(oldPassword));
            Contract.Requires(!string.IsNullOrWhiteSpace(newPassword));

            var crypto = GetCryptoProvider();
            var hashedOldPassword = crypto.HashPassword(oldPassword);

            if (hashedOldPassword != PasswordHash)
                return false;

            PasswordHash = crypto.HashPassword(newPassword);
            
            return true;
        }

        public virtual bool VerifyPassword(string plainTextPassword)
        {
            var crypto = GetCryptoProvider();
            var hasedPassword = crypto.HashPassword(plainTextPassword);

            return hasedPassword == PasswordHash;
        }

        public virtual void ResetPassword(string plainTextPassword)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(plainTextPassword));

            var crypto = GetCryptoProvider();
            PasswordHash = crypto.HashPassword(plainTextPassword);
        }

        private ICryptoProvider GetCryptoProvider()
        {
            return new SHA256CryptoProvider();
        }

        public static string SystemUser { get { return "System"; } }
    }
}
