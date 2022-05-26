using System.Net;

namespace EventTracker.BLL.Exceptions
{
    public class InvalidSubscriberException : HttpException
    {
        public InvalidSubscriberException(string message = "Cannot unsubscribe from an event you are not subscribed to!") 
            : base(message)
        {
            StatusCode = HttpStatusCode.Forbidden;
        }
    }
}