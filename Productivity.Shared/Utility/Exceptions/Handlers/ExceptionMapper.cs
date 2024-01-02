using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Exceptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.Exceptions.Handlers
{
    public static class ExceptionMapper
    {
        public static ErrorModel Map(Exception ex)
        {
            return ex switch
            {
                IErrorHandler => (ex as IErrorHandler)!.MapToResponce(),
                _ => new ErrorModel()
                {
                    Status = 400,
                    Message = ex.Message,
                    Errors = new()
                },
            };;
        }

        public static ErrorModel MapForHandler(Exception ex)
        {
            return ex switch
            {
                IErrorHandler => (ex as IErrorHandler)!.MapToResponce(),
                _ => new ErrorModel()
            };
        }
    }
}
