using System;
using System.Net;

namespace EventTrackerBlog.Application.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpException(string message) : base(message)
        {
        }
    }
}
