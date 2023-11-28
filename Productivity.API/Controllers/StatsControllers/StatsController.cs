using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Stats.Base;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;

namespace Productivity.API.Controllers.StatsControllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IProductivityStatsService _service;

        public StatsController(IProductivityStatsService service)
        {
            _service = service;
        }
        [HttpGet("Culture")]
        public async Task<ActionResult<CollectionDTO<RegionStatsModel>>> GetCultureStats([FromQuery] StatsDistinctQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetCultureStats(query, cancellationToken));
        }

        [HttpGet("Region")]
        public async Task<ActionResult<CollectionDTO<CultureStatsModel>>> GetReionStats([FromQuery] StatsDistinctQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetRegionStats(query, cancellationToken));
        }

        [HttpGet("Productivity")]
        public async Task<ActionResult<CollectionDTO<CultureStatsModel>>> GetProductivityStats([FromQuery] StatsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetProductivityStats(query, cancellationToken));
        }
    }
}
