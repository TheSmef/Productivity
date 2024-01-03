using LanguageExt.Common;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;

namespace Productivity.API.Services.Stats.Interfaces
{
    public interface IProductivityStatsService
    {
        public Task<Result<CollectionDTO<ProductivityStatsModel>>> GetStats(StatsQuery query, CancellationToken cancellationToken);
    }
}
