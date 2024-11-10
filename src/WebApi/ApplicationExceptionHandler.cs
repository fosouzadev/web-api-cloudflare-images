using Microsoft.AspNetCore.Diagnostics;

namespace WebApi;

public sealed class ApplicationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case ArgumentNullException:
            case ArgumentException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        ErrorResponse response = new() { Error = exception.Message };
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}