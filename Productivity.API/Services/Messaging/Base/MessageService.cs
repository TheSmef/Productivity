using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Productivity.Shared.Models.DTO.BrokerModels.Base;
using LanguageExt.Common;
using LanguageExt;
using Productivity.Shared.Services.Interfaces;

namespace Productivity.API.Services.Messaging.Base
{
    public class MessageService<T> : IMessagingService<T>
        where T : BaseReportModel
    {
        private readonly IRabbitMqService _service;
        private readonly string _exchange;
        private readonly string _routingKey;
        public MessageService(IRabbitMqService service, string queue, string exchange,
            string routingKey, string exchangeType)
        {
            _service = service;
            using (var connection = _service.CreateConnection())
            using (var model = connection.CreateModel())
            {
                model.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
                model.ExchangeDeclare(exchange, exchangeType, durable: true, autoDelete: false);
                model.QueueBind(queue, exchange, routingKey);
            }
            _routingKey = routingKey;
            _exchange = exchange;
        }


        public virtual Task<Result<Unit>> SendRequest(T request, CancellationToken cancellationToken)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));
            using (var connection = _service.CreateConnection())
            using (var model = connection.CreateModel())
            {
                model.BasicPublish(_exchange,
                                 _routingKey,
                                 basicProperties: null,
                                 body: body);
            }
            return Task.FromResult(new Result<Unit>(Unit.Default));
        }
    }
}
