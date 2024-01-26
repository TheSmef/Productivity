using Microsoft.AspNetCore.Mvc;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Constants;

namespace Productivity.API.Utility
{
    public static class ErrorResponceHandler
    {
        public static IActionResult GenerateErrorResponce(ActionContext context)
        {
            var error = new ErrorModel
            {
                Status = StatusCodes.Status400BadRequest,
                Message = ContextConstants.ValidationErrorTitle
            };
            var errors = context.ModelState.ToList();
            foreach (var errorItem in errors)
            {
                if (errorItem.Value != null)
                    error.Errors.AddRange(errorItem.Value.Errors.Select(x => x.ErrorMessage));
            }
            return new BadRequestObjectResult(error);
        }
    }
}
