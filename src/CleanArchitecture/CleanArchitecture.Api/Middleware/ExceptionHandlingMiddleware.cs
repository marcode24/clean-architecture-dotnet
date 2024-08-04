using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Middleware;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionHandlingMiddleware> _logger;
  public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      await _next(httpContext);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
      var exceptionDetails = GetExceptionDetails(ex);
      var problemDetails = new ProblemDetails
      {
        Status = exceptionDetails.Status,
        Type = exceptionDetails.Type,
        Title = exceptionDetails.Title,
        Detail = exceptionDetails.Detail,
        Instance = httpContext.Request.Path
      };

      if (exceptionDetails.Errors is not null)
      {
        problemDetails.Extensions.Add("errors", exceptionDetails.Errors);
      }

      httpContext.Response.StatusCode = exceptionDetails.Status;
      await httpContext.Response.WriteAsJsonAsync(problemDetails);
    }
  }

  private static ExceptionDetails GetExceptionDetails(Exception exception)
  {
    return exception switch
    {
      ValidationException validationException => new ExceptionDetails(
        StatusCodes.Status400BadRequest,
        "ValidationFailure",
        "Validation error",
        "One or more validation errors occurred.",
        validationException.Errors
      ),
      _ => new ExceptionDetails(
        StatusCodes.Status500InternalServerError,
        "ServerError",
        "An error occurred",
        exception.Message,
        null!
      )
    };
  }

  internal record ExceptionDetails(int Status, string Type, string Title, string Detail, IEnumerable<object> Errors);
}