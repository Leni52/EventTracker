using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

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

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("On ActionExecuted method: ");
        }


    }
}
