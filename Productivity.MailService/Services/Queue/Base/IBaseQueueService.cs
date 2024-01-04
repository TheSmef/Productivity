using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Services.Queue.Base
{
    public interface IBaseQueueService
    {
        public Task StartConsume(AsyncEventHandler<BasicDeliverEventArgs> handler);
    }
}
