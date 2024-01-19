using LanguageExt;
using LanguageExt.Common;
using Microsoft.IdentityModel.Tokens;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Messaging.Base;
using Productivity.API.Services.Messaging.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;
using Productivity.Shared.Services.Interfaces;
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
            List<string?> errors = [];
            if (!await _regionRepository.Exists(request.RegionId))
            {
                errors.Add(ContextConstants.RegionNotFound);
            }
            if (!await _cultureRepository.Exists(request.CultureId))
            {
                errors.Add(ContextConstants.CultureNotFound);
            }
            if (!errors.IsNullOrEmpty())
            {
                return new Result<Unit>(new DataException(errors, ContextConstants.ValidationErrorTitle));
            }
            if (_productivityRepository.GetItems(cancellationToken, x => x.Culture.Id == request.CultureId && x.Region.Id == request.RegionId).Count() < 2)
            {
                return new Result<Unit>(new QueryException(ContextConstants.ShortageOfData));
            }
            return await base.SendRequest(request, cancellationToken);
        }
    }
}
