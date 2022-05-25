using ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace ExceptionHandling.Handler
{
    public class ExceptionHandlerConfig
    {
        public ExceptionHandlerOptions CustomOptions
        {
            get => new ExceptionHandlerOptions()
            {
                ExceptionHandler = async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    Exception error = contextFeature.Error;
                    var response = context.Response;
                    if (error == null || error is not HttpException)
                    {
                        Exception initialError = new Exception("Internal server error");
                    }
                    HttpException exception = (HttpException)error;
                    var httpCode = exception.StatusCode;
                    context.Response.StatusCode = (int)httpCode;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(
                         new
                         {
                             ErrorMessage = "There was an exception",
                             ExceptionType = error.GetType().Name.ToString()
                         });
                    await response.WriteAsync(result);
                }
            };
        }
    }
}