using System.Net;
using System.Text.Json;

namespace Todo.WebAPI.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment environment
    )
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occured.");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                Details = _environment.IsDevelopment()? ex.Message : null
            };
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
        
    }
}