
namespace AtYourService.Core.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class SHA256CryptoProvider : ICryptoProvider
    {
        private const string Salt = "d1n074";

        public string HashPassword(string plainTextPassword)
        {
            var passwordWithSalt = GetPasswordWithSalt(plainTextPassword);

            var sha256Alg = new SHA256Managed();

            var hashBytes = sha256Alg.ComputeHash(passwordWithSalt);

            return Convert.ToBase64String(hashBytes);
        }

        private static byte[] GetPasswordWithSalt(string plainTextPassword)
        {
            var saltBytes = Encoding.UTF8.GetBytes(Salt);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainTextPassword);

            var plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            for (var i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (var i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            return plainTextWithSaltBytes;
        }
    }
}
