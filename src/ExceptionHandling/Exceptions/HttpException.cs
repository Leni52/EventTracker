using System;
using System.Net;

namespace ExceptionHandling.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpException(string message) : base(message)
        {
        }
    }
}
