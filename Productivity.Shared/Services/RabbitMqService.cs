using Microsoft.Extensions.Options;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Services.Interfaces;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using RabbitMQ.Client;

namespace Productivity.Shared.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateConnection()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory()
                {
                    UserName = _configuration.Username,
                    Password = _configuration.Password,
                    HostName = _configuration.HostName
                };
                factory.DispatchConsumersAsync = true;
                var connection = factory.CreateConnection();
                return connection;
            }
            catch (Exception ex)
            {
                throw new BrokerException(ContextConstants.NoConnectionToBroker, ex);
            }
        }
    }
}
