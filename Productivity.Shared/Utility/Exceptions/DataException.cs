using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.Exceptions
{
    public class DataException : Exception
    {
        public List<string?> Errors { get; set; } = new();
        public DataException(string message) : base(message) { }
        public DataException(string message, Exception innerExc) : base(message, innerExc) { }
        public DataException(List<string?> Errors, string message, Exception innerExc) : base(message, innerExc)
        {
            this.Errors = Errors;
        }

        public DataException(List<string?> Errors, string message) : base(message)
        {
            this.Errors = Errors;
        }
    }
}
