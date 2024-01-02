using LanguageExt;
using LanguageExt.Common;
using Productivity.Shared.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.Validators
{
    public static class ListValidator
    {
        public static Result<Unit> Validate<T>(List<T> items)
            where T : class
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null)
                {
                    return new Result<Unit>(new DataException($"Пустой элемент на строке {i + 1}"));
                }
                ValidationContext validationContext
                        = new ValidationContext(items[i]);
                List<ValidationResult> results = new();
                if (!Validator.TryValidateObject(items[i], validationContext, results, true))
                {
                    return new Result<Unit>(new DataException(results.Select(x => x.ErrorMessage).ToList(), $"Ошибка на строке {i + 1}"));
                }
            }
            return Unit.Default;
        }
    }
}
