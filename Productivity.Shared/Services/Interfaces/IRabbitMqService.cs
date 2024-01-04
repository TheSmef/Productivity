using RabbitMQ.Client;

namespace Productivity.Shared.Services.Interfaces
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }
}
