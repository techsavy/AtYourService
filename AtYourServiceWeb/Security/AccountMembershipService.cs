using System;
using System.Web.Security;

namespace AtYourService.Web.Security
{
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public bool ValidateUser(string email, string password)
        {
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return _provider.ValidateUser(email, password);
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                var currentUser = _provider.GetUser(email, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        //public MdmUser GetUser(bool userIsOnline)
        //{
        //    return (MdmUser)_provider.GetUser(HttpContext.Current.User.Identity.Name, userIsOnline);
        //}

        //public MdmUser GetUser(string userName, bool userIsOnline)
        //{
        //    return (MdmUser)_provider.GetUser(userName, userIsOnline);
        //}
    }
}