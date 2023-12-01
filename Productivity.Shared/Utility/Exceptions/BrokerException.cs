using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.Exceptions
{
    public class BrokerException : Exception
    {
        public BrokerException(string message) : base(message) { }
        public BrokerException(string message, Exception innerExc) : base(message, innerExc) { }
    }
}
