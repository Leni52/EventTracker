using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EventTrackerBlog.WebAPI.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger<LogActionFilter> _logger;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
            _logger.LogInformation("On ActionExecuting method started.");
            var controllerName = context.RouteData.Values["controller"].ToString();
            var actionName = context.RouteData.Values["action"].ToString();
            _logger.LogInformation($"Controller: {controllerName}, Action Name: {actionName}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var timeTaken = _stopwatch.Elapsed;
            _logger.LogInformation($"Used time: {timeTaken}");
            _logger.LogInformation("On ActionExecuted method finished.");
        }
    }
}
