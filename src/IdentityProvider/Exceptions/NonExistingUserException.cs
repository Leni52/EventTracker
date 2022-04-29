using System;

namespace IdentityProvider.Exceptions
{
    public class NonExistingUserException : Exception
    {
        public NonExistingUserException(string message)
            : base(message)
        {
        }
    }
}
