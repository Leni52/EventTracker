using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EventTrackerBlog.WebAPI.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly ILogger<ActionFilterAttribute> _logger;
        public ActionFilterAttribute(ILogger<ActionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("On ActionExecuting method: ");
            var controllerName = context.RouteData.Values["controller"].ToString();
            var actionName = context.RouteData.Values["action"].ToString();
            _logger.LogInformation($"Controller: {controllerName}, Action Name: {actionName}");
            Log("log.txt", $"Controller: {controllerName}, Action Name: {actionName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("On ActionExecuted method: ");
            Log("log.txt", "OnActionExecuted method executed.");
        }

        public void Log(string fileName, string content)
        {
            using (StreamWriter w = File.AppendText(fileName))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine(content);
                w.WriteLine("-------------------------------");
            }
        }
    }
}
