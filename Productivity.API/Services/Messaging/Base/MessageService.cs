using Productivity.API.Services.Messaging.Base.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Productivity.Shared.Models.DTO.BrokerModels.Base;

namespace Productivity.API.Services.Messaging.Base
{
    public class MessageService<T> : IMessagingService<T>
        where T : BaseReportModel
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        private readonly string _exchange;
        private readonly string _routingKey;
        public MessageService(IRabbitMqService service, string queue, string exchange,
            string routingKey, string exchangeType)
        {
            _connection = service.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare(exchange, exchangeType, durable: true, autoDelete: false);
            _model.QueueBind(queue, exchange, routingKey);
            _routingKey = routingKey;
            _exchange = exchange;
        }


        public virtual Task SendRequest(T request, CancellationToken cancellationToken)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));
            _model.BasicPublish(_exchange,
                                 _routingKey,
                                 basicProperties: null,
                                 body: body);
            return Task.CompletedTask;
        }
    }
}
