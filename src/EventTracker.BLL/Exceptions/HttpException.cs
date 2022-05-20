using System;
using System.Net;

namespace EventTracker.BLL.Exceptions
{
    public abstract class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpException(string message) : base(message)
        {
        }
    }
}
