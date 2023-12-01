using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Messaging.Base;
using Productivity.API.Services.Messaging.Base.Interfaces;
using Productivity.API.Services.Messaging.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using RabbitMQ.Client;

namespace Productivity.API.Services.Messaging
{
    public class ProductivityReportMessagingService : MessageService<ProductivityReportModel>, 
        IProductivityReportMessagingService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICultureRepository _cultureRepository;
        private readonly IProductivityRepository _productivityRepository;
        public ProductivityReportMessagingService(IRabbitMqService service, 
            ICultureRepository cultureRepository, IRegionRepository regionRepository, 
            IProductivityRepository productivityRepository)
            : base(service, "ProductivityQueue", "ReportExchange", "productivity", ExchangeType.Direct)
        {
            _cultureRepository = cultureRepository;
            _regionRepository = regionRepository;
            _productivityRepository = productivityRepository;
        }

        public override async Task SendRequest(ProductivityReportModel request, CancellationToken cancellationToken)
        {
            if (!await _regionRepository.Exists(request.RegionId))
            {
                throw new QueryException(ContextConstants.RegionNotFound);
            }
            if (!await _cultureRepository.Exists(request.CultureId))
            {
                throw new QueryException(ContextConstants.CultureNotFound);
            }
            if (_productivityRepository.GetItems(cancellationToken, x => x.Culture.Id == request.CultureId && x.Region.Id == request.RegionId).Count() < 2)
            {
                throw new QueryException(ContextConstants.ShortageOfData);
            }
            await base.SendRequest(request, cancellationToken);
        }
    }
}
