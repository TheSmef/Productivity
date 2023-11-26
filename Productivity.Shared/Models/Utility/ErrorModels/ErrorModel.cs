using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Utility.ErrorModels
{
    public class ErrorModel
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string?> Errors { get; set; } = new();
    }
}
