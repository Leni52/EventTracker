using EventTracker.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventTracker.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    
                    case ItemDoesNotExistException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;                 
                        
                    case ItemIsAlreadyUsedException:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;

                    default:
                        // Unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new
                {
                    ExceptionType = $"{error.GetType().Name}",
                    DefaultMessage = "There was an exception while trying to process your requests",
                    SpecificMessage = $"{error.Message}"
                });

                await response.WriteAsync(result);
            }
        }
    }
}
