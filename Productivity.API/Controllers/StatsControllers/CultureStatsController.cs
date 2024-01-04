using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.StatsControllers.Base;
using Productivity.API.Services.Stats.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Exceptions.Handlers;

namespace Productivity.API.Controllers.StatsControllers
{
    [Route("[controller]")]
    [ApiController]
    public class CultureStatsController : BaseStatsController<StatsDistinctQuery, RegionStatsModel>
    {
        public CultureStatsController(ICultureStatsService service) : base(service) { }

        [ProducesResponseType(typeof(CollectionDTO<RegionStatsModel>), StatusCodes.Status200OK)]
        public override Task<ActionResult<CollectionDTO<RegionStatsModel>>> GetStats([FromQuery] StatsDistinctQuery query, CancellationToken cancellationToken)
        {
            return base.GetStats(query, cancellationToken);
        }
    }
}
