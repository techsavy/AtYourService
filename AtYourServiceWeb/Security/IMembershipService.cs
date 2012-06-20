
namespace AtYourService.Web.Security
{
    public interface IMembershipService
    {
        bool ValidateUser(string email, string password);
        bool ChangePassword(string email, string oldPassword, string newPassword);
        //User GetUser(bool userIsOnline);
        //User GetUser(string userName, bool userIsOnline);
    }
}