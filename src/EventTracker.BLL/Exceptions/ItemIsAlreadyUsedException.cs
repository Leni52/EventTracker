using System;
using System.Net;

namespace EventTracker.BLL.Exceptions
{
    public class ItemIsAlreadyUsedException : HttpException
    {

        public ItemIsAlreadyUsedException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}