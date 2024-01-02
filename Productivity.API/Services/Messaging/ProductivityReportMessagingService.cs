using LanguageExt;
using LanguageExt.Common;
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
            : base(service, ContextConstants.ProductivityQueue, ContextConstants.ReportExchange,
                  ContextConstants.ProductivityRoutingKey, ExchangeType.Direct)
        {
            _cultureRepository = cultureRepository;
            _regionRepository = regionRepository;
            _productivityRepository = productivityRepository;
        }

        public override async Task<Result<Unit>> SendRequest(ProductivityReportModel request, CancellationToken cancellationToken)
        {
            if (!await _regionRepository.Exists(request.RegionId))
            {
                return new Result<Unit>(new QueryException(ContextConstants.RegionNotFound));
            }
            if (!await _cultureRepository.Exists(request.CultureId))
            {
                return new Result<Unit>(new QueryException(ContextConstants.CultureNotFound));
            }
            if (_productivityRepository.GetItems(cancellationToken, x => x.Culture.Id == request.CultureId && x.Region.Id == request.RegionId).Count() < 2)
            {
                return new Result<Unit>(new QueryException(ContextConstants.ShortageOfData));
            }
            return await base.SendRequest(request, cancellationToken);
        }
    }
}
