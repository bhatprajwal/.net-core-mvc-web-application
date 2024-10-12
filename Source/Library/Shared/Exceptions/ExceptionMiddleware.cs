using Exceptions.Dtos;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    #region Private Member
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    #endregion

    #region Constructor
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger
    , IHostEnvironment env
)
    {
        _logger = logger;
        _env = env;
    }
    #endregion

    #region Public Function
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    #endregion

    #region Private Function
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = _env.IsDevelopment()
            ? new CustomResponse(context.Response.StatusCode, exception.Message, exception.StackTrace?.ToString())
            : new CustomResponse(context.Response.StatusCode, "Please contact Administrator");

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    } 
    #endregion
}
