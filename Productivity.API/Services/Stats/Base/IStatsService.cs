using LanguageExt.Common;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;

namespace Productivity.API.Services.Stats.Base
{
    public interface IStatsService<T, TDTO>
        where T : StatsQuery
        where TDTO : class 
    {
        public Task<Result<CollectionDTO<TDTO>>> GetStats(T query, CancellationToken cancellationToken);
    }
}
