using Productivity.API.Services.Messaging.Base;
using Productivity.API.Services.Messaging.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Productivity.API.Services.Messaging.Base.Interfaces;
using System.Text;
using System.Text.Json;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.Constants;

namespace Productivity.API.Services.Messaging
{
    public class CultureReportMessagingService : MessageService<CultureReportModel>, ICultureReportMessagingService
    {
        private readonly IRegionRepository _repository;
        public CultureReportMessagingService(IRabbitMqService service, IRegionRepository repository)
            : base(service, "CultureQueue", "ReportExchange", "culture", ExchangeType.Direct)
        {
            _repository = repository;
        }

        public async override Task SendRequest(CultureReportModel request, CancellationToken cancellationToken)
        {
            if (!await _repository.Exists(request.RegionId))
            {
                throw new QueryException(ContextConstants.RegionNotFound);
            }
            await base.SendRequest(request, cancellationToken);
        }
    }
}
