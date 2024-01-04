using Productivity.MailService.Services.Queue.Base;
using Productivity.MailService.Services.Queue.Interfaces;
using Productivity.Shared.Services.Interfaces;
using Productivity.Shared.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Services.Queue
{
    public class MailQueueService : BaseQueueService, IMailQueueService
    {
        public MailQueueService(IRabbitMqService service) : base(service, ContextConstants.MailQueue) { }
    }
}
