using Framework.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApplication.Api;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            BaseApplicationException exception => exception.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

        _logger.LogError(context.Exception, context.Exception.Message);
        if (context.Exception.GetBaseException() is var inner && inner is not null)
            _logger.LogError(inner, inner.Message);

        var body = new
        {
            Success = false,
            ErrorMessage = context.Exception.Message,
            InnerMessage = context.Exception.GetBaseException().Message
        };
        context.Result = new ObjectResult(body)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}