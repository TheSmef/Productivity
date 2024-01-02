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
using LanguageExt.Common;
using LanguageExt;

namespace Productivity.API.Services.Messaging
{
    public class CultureReportMessagingService : MessageService<CultureReportModel>, ICultureReportMessagingService
    {
        private readonly IRegionRepository _repository;
        public CultureReportMessagingService(IRabbitMqService service, IRegionRepository repository)
            : base(service, ContextConstants.CultureQueue, ContextConstants.ReportExchange,
                  ContextConstants.CultureRoutingKey, ExchangeType.Direct)
        {
            _repository = repository;
        }

        public async override Task<Result<Unit>> SendRequest(CultureReportModel request, CancellationToken cancellationToken)
        {
            if (!await _repository.Exists(request.RegionId))
            {
                return new Result<Unit>(new QueryException(ContextConstants.RegionNotFound));
            }
            return await base.SendRequest(request, cancellationToken);
        }
    }
}
