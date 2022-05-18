using System;
using System.Net;

namespace EventTracker.BLL.Exceptions
{
    public class ItemIsAlreadyUsedException : HttpException
    {

        public ItemIsAlreadyUsedException(string message = "Item is already in use.") : base(message)
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}