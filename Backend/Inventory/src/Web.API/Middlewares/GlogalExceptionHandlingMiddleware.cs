using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Middlewares;

public class GlogalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlogalExceptionHandlingMiddleware> _logger;
    public GlogalExceptionHandlingMiddleware(ILogger<GlogalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problemDetails = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internar server error has ocurred"
            };

            string json = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}
