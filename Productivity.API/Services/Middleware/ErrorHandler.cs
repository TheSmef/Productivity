using Microsoft.AspNetCore.Diagnostics;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Exceptions;

namespace Productivity.API.Services.Middleware
{
    public class ErrorHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int status, string message, List<string?> errors) = exception switch
            {
                QueryException => (400, exception.Message, new List<string?>()),
                DataException => (400, exception.Message, ((DataException)exception).Errors),
                _ => (-1, string.Empty, new List<string?>())
            };
            if (status == -1)
            {
                return false;
            }
            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(new ErrorModel()
            {
                Message = message,
                Status = status,
                Errors = errors ?? new()
            }, cancellationToken);
            return true;
        }
    }
}
