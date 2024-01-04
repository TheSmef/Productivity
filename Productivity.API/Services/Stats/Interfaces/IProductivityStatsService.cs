using LanguageExt.Common;
using Productivity.API.Services.Stats.Base;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;

namespace Productivity.API.Services.Stats.Interfaces
{
    public interface IProductivityStatsService : IStatsService<StatsQuery, ProductivityStatsModel> { }
}
