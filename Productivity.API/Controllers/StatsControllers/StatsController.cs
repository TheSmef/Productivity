using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Stats.Base;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;
using Productivity.Shared.Utility.Exceptions.Handlers;

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
            var result = await _service.GetCultureStats(query, cancellationToken);
            return result.Match<ActionResult<CollectionDTO<RegionStatsModel>>>(
                succ =>
                {
                    return Ok(succ);
                },
                err =>
                {
                    return BadRequest(ExceptionMapper.Map(err));
                }
                );
        }

        [HttpGet("Region")]
        public async Task<ActionResult<CollectionDTO<CultureStatsModel>>> GetReionStats([FromQuery] StatsDistinctQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetRegionStats(query, cancellationToken);
            return result.Match<ActionResult<CollectionDTO<CultureStatsModel>>>(
                succ =>
                {
                    return Ok(succ);
                },
                err =>
                {
                    return BadRequest(ExceptionMapper.Map(err));
                }
                );
        }

        [HttpGet("Productivity")]
        public async Task<ActionResult<CollectionDTO<ProductivityStatsModel>>> GetProductivityStats([FromQuery] StatsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetProductivityStats(query, cancellationToken);
            return result.Match<ActionResult<CollectionDTO<ProductivityStatsModel>>>(
                succ =>
                {
                    return Ok(succ);
                },
                err =>
                {
                    return BadRequest(ExceptionMapper.Map(err));
                }
                );
        }
    }
}
