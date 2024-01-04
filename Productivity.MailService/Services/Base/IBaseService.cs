using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Services.Base
{
    public interface IBaseService
    {
        public Task Send(string record, CancellationToken cancellationToken);
    }
}
