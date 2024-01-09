using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Productivity.MailService.Data.Context;
using Productivity.MailService.Services.Interfaces;
using Productivity.MailService.Services.Queue.Interfaces;
using Productivity.MailService.Services.Sender;
using Productivity.Shared.Models.DTO.BrokerModels.SendModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Services.Interfaces;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Productivity.MailService.Services
{
    public class MailWorker : BackgroundService
    {
        private readonly IMailQueueService _service;
        private readonly IServiceScopeFactory _factory;

        public MailWorker(IMailQueueService service,             
            IServiceScopeFactory factory)
        {
            _service = service;
            _factory = factory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _service.StartConsume(
                (sender, args) =>
                {
                    var scope = _factory.CreateScope();
                    var service = scope.ServiceProvider.GetService<IMailService>()!;
                    return service.Send(Encoding.UTF8.GetString(args.Body.ToArray()), stoppingToken);
                }
            );
        }
    }
}
