using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;

namespace Productivity.API.Services.Stats.Base
{
    public interface IProductivityStatsService
    {
        public Task<CollectionDTO<RegionStatsModel>> GetCultureStats(StatsDistinctQuery query, CancellationToken cancellationToken);
        public Task<CollectionDTO<ProductivityStatsModel>> GetProductivityStats(StatsQuery query, CancellationToken cancellationToken);
        public Task<CollectionDTO<CultureStatsModel>> GetRegionStats(StatsDistinctQuery query, CancellationToken cancellationToken);
    }
}
