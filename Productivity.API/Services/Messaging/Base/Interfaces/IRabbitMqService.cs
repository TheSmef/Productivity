using RabbitMQ.Client;

namespace Productivity.API.Services.Messaging.Base.Interfaces
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }
}
