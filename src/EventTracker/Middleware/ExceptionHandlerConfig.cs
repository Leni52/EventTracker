using EventTracker.BLL.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace EventTracker.Middleware
{
    public class ExceptionHandlerConfig
    {
        public ExceptionHandlerOptions CustomOptions
        {
            get
            {
                return new ExceptionHandlerOptions()
                {
                    ExceptionHandler = async context =>
                    {
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        Exception error = contextFeature.Error;                     
                        HttpException httpException = (HttpException)error;
                        var httpCode = httpException.StatusCode;

                        if (error == null)
                        {
                            Exception initialError = new Exception("internal error");
                        }
                        
                        context.Response.StatusCode = (int)httpCode;
                        context.Response.ContentType = "application/json";
                       
                        var result = JsonSerializer.Serialize(new
                        {
                            ExceptionType = $"{error.GetType().Name}",
                            DefaultMessage = "There was an exception trying to process your request",
                            SpecificMessage = $"{error.Message}"
                        });
                         context.Response.WriteAsync(result).Wait();
                    }
                };
            }
        }
    }
}
