using System.Net;
using System.Text.Json;
using GymProgress.Shared.Exceptions;
using GymProgress.Shared.Wrappers;

namespace GymProgress.API.Middleware;

/// <summary>
/// Single place where every exception in the pipeline gets translated into
/// the ApiResponse envelope. Controllers stay free of try/catch blocks,
/// and every error the Flutter app receives has the exact same JSON shape.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message, errors) = exception switch
        {
            FluentValidation.ValidationException validationException => (
                (int)HttpStatusCode.BadRequest,
                "One or more validation failures occurred.",
                validationException.Errors.Select(e => e.ErrorMessage)),

            AppException appException => (
                appException.StatusCode,
                appException.Message,
                Enumerable.Empty<string>()),

            _ => (
                (int)HttpStatusCode.InternalServerError,
                "An unexpected error occurred. Please try again later.",
                Enumerable.Empty<string>())
        };

        if (statusCode == (int)HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception occurred while processing {Path}", context.Request.Path);
        }
        else
        {
            _logger.LogWarning("Handled exception while processing {Path}: {Message}", context.Request.Path, exception.Message);
        }

        context.Response.StatusCode = statusCode;

        var response = ApiResponse<object>.FailureResponse(message, errors);
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        await context.Response.WriteAsync(json);
    }
}
