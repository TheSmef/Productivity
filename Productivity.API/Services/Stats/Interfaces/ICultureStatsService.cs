using LanguageExt.Common;
using Productivity.API.Services.Stats.Base;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Services.Stats.Interfaces
{
    public interface ICultureStatsService : IStatsService<StatsDistinctQuery, RegionStatsModel> { }
}
