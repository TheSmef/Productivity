using Microsoft.Extensions.Options;
using Productivity.API.Services.Messaging.Base.Interfaces;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using RabbitMQ.Client;

namespace Productivity.API.Services.Messaging.Base
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateChannel()
        {
            try
            {
                ConnectionFactory connection = new ConnectionFactory()
                {
                    UserName = _configuration.Username,
                    Password = _configuration.Password,
                    HostName = _configuration.HostName
                };
                connection.DispatchConsumersAsync = true;
                var channel = connection.CreateConnection();
                return channel;
            }
            catch (Exception ex)
            {
                throw new BrokerException(ContextConstants.NoConnectionToBroker, ex);
            }
        }
    }
}
