using Productivity.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Utility.Base
{
    public class StatsQuery
    {
        [GuidNotNull(ErrorMessage = "Идентификатор параметра статистики не может быть пустым")]
        [Required(ErrorMessage = "Идентификатор параметра статистики не может быть пустым")]
        public Guid Id { get; set; } = Guid.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Неправильный формат отступа в строке запроса")]
        public int Skip { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Неправильный формат количества записей в строке запроса")]
        public int Top { get; set; } = 0;
    }
}
