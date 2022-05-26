using System.Net;

namespace ExceptionHandling.Exceptions
{
    public class ItemIsAlreadyUsedException : HttpException
    {
        public ItemIsAlreadyUsedException(string message = "Item does not exist.") : base(message)
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
