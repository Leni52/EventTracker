using System;
using System.Net;

namespace EventTracker.BLL.Exceptions
{

    public class ItemDoesNotExistException : HttpException
    {
        public ItemDoesNotExistException(HttpStatusCode statusCode, string message):base(message)
        {
          StatusCode = HttpStatusCode.NotFound;            
        }
    }
}