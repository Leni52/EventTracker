using System;
using System.Runtime.Serialization;

namespace IdentityProvider.Exceptions
{

    public class RoleDoesNotExistException : Exception
    {
       
        public RoleDoesNotExistException(string message) : base(message)
        {
        }        
    }
}