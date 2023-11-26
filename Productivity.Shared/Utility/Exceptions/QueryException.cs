using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.Exceptions
{
    public class QueryException : Exception
    {
        public QueryException(string message) : base(message) { }
        public QueryException(string message, Exception innerExc) : base(message, innerExc) { }
    }
}
