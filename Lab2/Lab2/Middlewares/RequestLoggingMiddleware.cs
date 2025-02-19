namespace Lab2.Middlewares;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        request.EnableBuffering();

        // Read the body
        string requestBody = "";
        if (request.ContentLength != null && request.ContentLength > 0 && request.Body.CanRead)
        {
            request.Body.Position = 0;
            using StreamReader reader = new StreamReader(request.Body, Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false, leaveOpen: true);
                
            requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            
        }

        // Read the request
        var method = request.Method;
        var path = request.Path;
        var queryString = request.QueryString.HasValue ? request.QueryString.Value : "";
        var headers = request.Headers;

        // Log 
        var requestTimestamp = DateTime.UtcNow;
        logger.LogInformation(
            "Incoming Request at {Timestamp}: Method={Method}, Path={Path}, QueryString={QueryString}, Headers={Headers}, Body={Body}",
            requestTimestamp, method, path, queryString, headers, requestBody);
        
        // Next delegate
        await next(context);

        // Read the response
        var responseStatusCode = context.Response.StatusCode;
        var responseTimestamp = DateTime.UtcNow;
        logger.LogInformation(
            "Outgoing Response at {Timestamp}: StatusCode={StatusCode}",
            responseTimestamp, responseStatusCode);
    }
}
