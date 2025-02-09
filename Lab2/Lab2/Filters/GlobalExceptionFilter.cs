namespace lab2.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ArgumentException argEx)
        {
            _logger.LogWarning(argEx, "A client error occurred: {Message}", argEx.Message);

            context.Result = new BadRequestObjectResult(new 
            {
                Error = "Invalid input provided. Please make sure the user information is correct."
            });
            context.ExceptionHandled = true;
            return;
        }
        
        if (context.Exception is ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "A client error occurred: {Message}", ex.Message);

            context.Result = new BadRequestObjectResult(new 
            {
                Error = "Make sure your input is not null and try again."
            });
            context.ExceptionHandled = true;
            return;
        }
        
        // I am only throwing these exceptions
        // If I had other Ex, I would handle here
        
        _logger.LogError(context.Exception, "An unexpected error occurred.");
        context.Result = new ObjectResult(new 
        {
            Error = "An unexpected error occurred. Please try again later."
        })
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}