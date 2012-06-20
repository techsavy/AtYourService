
namespace AtYourService.Core.Security
{
    public interface ICryptoProvider
    {
        string HashPassword(string plainTextPassword);
    }
}
