using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Services.Stats.Base
{
    public interface IProductivityStatsService
    {
        public Task<CollectionDTO<CultureStatsModel>> GetCultureStats(QuerySupporter specification, CancellationToken cancellationToken);
        public Task<CollectionDTO<ProductivityStatsModel>> GetProductivityStats(QuerySupporter specification, CancellationToken cancellationToken);
        public Task<CollectionDTO<RegionStatsModel>> GetRegionStats(QuerySupporter specification, CancellationToken cancellationToken);
    }
}
