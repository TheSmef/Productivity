using Productivity.Shared.Services.Interfaces;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Productivity.MailService.Services.Queue.Base
{
    public class BaseQueueService : IBaseQueueService
    {
        private readonly IRabbitMqService _service;
        private readonly string _queueName;

        public BaseQueueService(IRabbitMqService service, string queuename)
        {
            _service = service;
            _queueName = queuename;
        }
        public Task StartConsume(AsyncEventHandler<BasicDeliverEventArgs> handler)
        {
            var channel = _service.CreateConnection().CreateModel();
            channel.QueueDeclare(queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += handler;

            channel.BasicConsume(
                    queue: _queueName,
                    autoAck: true,
                    consumer: consumer,
                    consumerTag: _queueName,
                    noLocal: true,
                    exclusive: false,
                    arguments: null
                );
            return Task.CompletedTask;
        }
    }
}
