using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
