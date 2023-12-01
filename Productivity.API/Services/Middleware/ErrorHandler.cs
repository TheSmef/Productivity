using Microsoft.AspNetCore.Diagnostics;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Exceptions;

namespace Productivity.API.Services.Middleware
{
    public class ErrorHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ErrorModel responce = exception switch
            {
                QueryException => new ErrorModel() { 
                    Status = 400, 
                    Message = exception.Message, 
                    Errors = new() },
                DataException => new ErrorModel() { 
                    Status = 400, 
                    Message = exception.Message, 
                    Errors = ((DataException)exception).Errors },
                BrokerException => new ErrorModel() {
                    Status = 400,
                    Message = exception.Message,
                    Errors = new() },
                _ => new ErrorModel()
            };
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
