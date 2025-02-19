namespace lab2.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    // Before execution
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        _logger.LogInformation("Starting execution of action: {ActionName}", actionName);

        foreach (var param in context.ActionArguments)
        {
            _logger.LogInformation("Parameter: {Key} = {@Value}", param.Key, param.Value);
        }
    }

    // After execution
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        if (context.Result is ObjectResult objectResult)
        {
            _logger.LogInformation(
                "Finished action: {ActionName}. Status Code: {StatusCode}, Result: {@Value}",
                actionName, objectResult.StatusCode, objectResult.Value);
        }
        else if (context.Result is StatusCodeResult statusCodeResult)
        {
            _logger.LogInformation(
                "Finished action: {ActionName}. Status Code: {StatusCode}",
                actionName, statusCodeResult.StatusCode);
        }
        else
        {
            _logger.LogInformation(
                "Finished action: {ActionName}. Result: {@Result}",
                actionName, context.Result);
        }

        _logger.LogInformation("Completed execution of action: {ActionName}", actionName);
    }
}
