using System;

namespace EventTracker.BLL.Exceptions
{

    public class ItemDoesNotExistException : Exception
    {

        public ItemDoesNotExistException(string message) : base(message)
        {
        }

    }
}