using System;
using System.Runtime.Serialization;

namespace IdentityProvider.Exceptions
{
   
    public class NoRolesException : Exception
    {
        public NoRolesException(string message) : base(message)
        {
        }
    }
}