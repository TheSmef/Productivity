using LanguageExt.Common;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Services.Stats.Interfaces
{
    public interface IRegionStatsService
    {
        public Task<Result<CollectionDTO<CultureStatsModel>>> GetStats(StatsDistinctQuery query, CancellationToken cancellationToken);
    }
}
