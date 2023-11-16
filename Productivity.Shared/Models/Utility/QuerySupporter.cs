namespace Productivity.Shared.Models.Utility
{
    public class QuerySupporter
    {
        public string? Filter { get; set; }
        public string[]? FilterParams { get; set; }
        public string? OrderBy { get; set; }
        public int Skip { get; set; } = -1;
        public int Top { get; set; } = -1;
    }
}
