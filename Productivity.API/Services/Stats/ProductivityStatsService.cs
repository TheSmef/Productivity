using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Stats.Base;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility; 

namespace Productivity.API.Services.Stats
{
    public class ProductivityStatsService : IProductivityStatsService
    {
        private readonly ICultureRepository _cultureRepository;
        private readonly IProductivityRepository _productivityRepository;
        private readonly IRegionRepository _regionRepository;
        public Task<CollectionDTO<CultureStatsModel>> GetCultureStats(QuerySupporter specification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CollectionDTO<ProductivityStatsModel>> GetProductivityStats(QuerySupporter specification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CollectionDTO<RegionStatsModel>> GetRegionStats(QuerySupporter specification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
