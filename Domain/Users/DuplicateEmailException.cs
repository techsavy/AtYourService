
namespace AtYourService.Domain.Users
{
    using System;

    public class DuplicateEmailException : ApplicationException
    {
        public DuplicateEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateEmailException(Exception innerException)
            : this("Email already exists", innerException)
        {
        }
    }
}
