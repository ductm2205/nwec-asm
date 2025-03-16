using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using core.Exceptions;
using Newtonsoft.Json;

namespace api.Middleware;

public class GlobalExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext request)
    {
        try
        {
            await _next(request);
        }
        catch (System.Exception ex)
        {
            await HandleExceptionAsync(request, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext request, Exception ex)
    {
        _logger.LogError(ex, "An error was thrown");

        var statusCode = ex switch
        {
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            ArgumentException => HttpStatusCode.BadRequest,
            ValidationException => HttpStatusCode.BadRequest,
            EntityNotFoundException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError,
        };

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = ex.Message
        };

        request.Response.ContentType = "application/json";
        request.Response.StatusCode = response.StatusCode;

        var jsonResp = JsonConvert.SerializeObject(response);

        await request.Response.WriteAsync(jsonResp);
    }
}
