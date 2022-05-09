using System;

namespace IdentityProvider.Exceptions
{

    public class NoUsersWithSuchRoleException : Exception
    {

        public NoUsersWithSuchRoleException(string message) : base(message)
        {
        }
    }
}