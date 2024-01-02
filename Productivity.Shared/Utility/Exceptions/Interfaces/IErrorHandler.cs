using Productivity.Shared.Models.Utility.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.Exceptions.Interfaces
{
    public interface IErrorHandler
    {
        public ErrorModel MapToResponce();
    }
}
