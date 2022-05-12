using System;
using System.Runtime.Serialization;

namespace EventTracker.BLL.Exceptions
{   
    public class ItemIsAlreadyUsedException : Exception
    {
       
        public ItemIsAlreadyUsedException(string message) : base(message)
        {
        }

    }
}