using System.ComponentModel.DataAnnotations;

namespace Productivity.Shared.Models.Utility
{
    public class QuerySupporter
    {
        public string? Filter { get; set; }
        public string[]? FilterParams { get; set; }
        public string? OrderBy { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Неправильный формат отступа в строке запроса")]
        public int Skip { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Неправильный формат количества записей в строке запроса")]
        public int Top { get; set; } = 0;
    }
}
