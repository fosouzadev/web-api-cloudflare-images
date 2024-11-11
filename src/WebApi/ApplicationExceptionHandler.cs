using Microsoft.AspNetCore.Diagnostics;

namespace WebApi;

public sealed class ApplicationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ErrorResponse response = new() { Error = exception.Message };

        switch (exception)
        {
            case ArgumentNullException:
            case ArgumentException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;
            case HttpRequestException ex:
                httpContext.Response.StatusCode = (int)ex.StatusCode;
                break;
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}