using System.Net;

namespace ExceptionHandling.Exceptions
{
    public class ItemDoesNotExistException : HttpException
    {
        public ItemDoesNotExistException(string message = "Item does not exist.") : base(message)
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
