using Productivity.Shared.Attributes;
using Productivity.Shared.Models.Utility.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Utility
{
    public class StatsDistinctQuery : StatsQuery
    {
        [Required(ErrorMessage = "Год запроса обязателен для ввода!")]
        [DependableRange(10, 0, ErrorMessage = "Год запроса должен быть между {1} и {2}")]
        public int Year { get; set; }
    }
}
