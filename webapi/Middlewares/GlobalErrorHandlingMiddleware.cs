using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace webapi.Middlewares
{
    public class GlobalErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
        public GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger) => 
            _logger = logger;
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        { 
            try
            {
                await next(context);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = (int)e.StatusCode;


                ProblemDetails problemDetails = new()
                {
                    Status = (int)e.StatusCode,
                    Detail = e.Message,
                };

                string json = JsonSerializer.Serialize(problemDetails);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
