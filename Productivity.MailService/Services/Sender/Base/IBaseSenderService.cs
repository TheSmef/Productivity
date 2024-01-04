using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Services.Sender.Base
{
    public interface IBaseSenderService
    {
        public Task Send(Mail record);
    }
}
