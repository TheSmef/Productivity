using Microsoft.AspNetCore.Diagnostics;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.Exceptions.Handlers;

namespace Productivity.API.Services.Middleware
{
    public class ErrorHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ErrorModel responce = ExceptionMapper.MapForHandler(exception);
            if (responce.Status == -1)
            {
                return false;
            }
            httpContext.Response.StatusCode = responce.Status;
            await httpContext.Response.WriteAsJsonAsync(responce, cancellationToken);
            return true;
        }
    }
}
