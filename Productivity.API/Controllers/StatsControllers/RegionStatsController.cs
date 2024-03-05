using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Productivity.API.Controllers.StatsControllers.Base;
using Productivity.API.Services.Stats.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions.Handlers;

namespace Productivity.API.Controllers.StatsControllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionStatsController : BaseStatsController<StatsDistinctQuery, CultureStatsModel>
    {
        public RegionStatsController(IRegionStatsService service) : base(service) { }

        [ProducesResponseType(typeof(CollectionDTO<CultureStatsModel>), StatusCodes.Status200OK)]
        [OutputCache(Tags = [ContextConstants.ProductivityCacheTag])]
        public override Task<ActionResult<CollectionDTO<CultureStatsModel>>> GetStats([FromQuery] StatsDistinctQuery query, CancellationToken cancellationToken)
        {
            return base.GetStats(query, cancellationToken);
        }
    }
}
