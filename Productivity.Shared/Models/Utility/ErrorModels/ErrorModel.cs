using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.Shared.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Utility.ErrorModels
{
    public class ErrorModel
    {
        public int Status { get; set; } = -1;
        public string Message { get; set; } = string.Empty;
        public List<string?> Errors { get; set; } = new();

        public static IActionResult GenerateErrorResponce(ActionContext context)
        {
            var error = new ErrorModel();
            error.Status = StatusCodes.Status400BadRequest;
            error.Message = ContextConstants.ValidationErrorTitle;
            var errors = context.ModelState.ToList();
            foreach (var errorItem in errors)
            {
                error.Errors.AddRange(errorItem.Value.Errors.Select(x => x.ErrorMessage));
            }
            return new BadRequestObjectResult(error);
        }
    }
}
