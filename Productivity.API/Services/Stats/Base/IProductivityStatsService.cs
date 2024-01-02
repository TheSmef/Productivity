using LanguageExt.Common;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;

namespace Productivity.API.Services.Stats.Base
{
    public interface IProductivityStatsService
    {
        public Task<Result<CollectionDTO<RegionStatsModel>>> GetCultureStats(StatsDistinctQuery query, CancellationToken cancellationToken);
        public Task<Result<CollectionDTO<ProductivityStatsModel>>> GetProductivityStats(StatsQuery query, CancellationToken cancellationToken);
        public Task<Result<CollectionDTO<CultureStatsModel>>> GetRegionStats(StatsDistinctQuery query, CancellationToken cancellationToken);
    }
}
