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
        public static void Validate<T>(List<T> items)
            where T : class
        {
            foreach (var item in items)
            {
                if (item == null)
                {
                    throw new DataException($"Пустой элемент на строке {items.FindIndex(x => x == null)}");
                }
                ValidationContext validationContext
                        = new ValidationContext(item);
                List<ValidationResult> results = new();
                if (!Validator.TryValidateObject(item, validationContext, results, true))
                {
                    throw new DataException(results.Select(x => x.ErrorMessage).ToList(), $"Ошибка на строке {items.FindIndex(x => x == item) + 1}");
                }
            }
        }
    }
}
