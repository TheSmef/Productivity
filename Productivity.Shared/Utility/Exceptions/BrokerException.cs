using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Exceptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.Exceptions
{
    public class BrokerException : Exception, IErrorHandler
    {
        public BrokerException(string message) : base(message) { }
        public BrokerException(string message, Exception innerExc) : base(message, innerExc) { }

        public ErrorModel MapToResponce()
        {
            return new ErrorModel()
            {
                Status = 400,
                Errors = new(),
                Message = this.Message
            };
        }
    }
}
