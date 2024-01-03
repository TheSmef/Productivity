using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Stats.Interfaces;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Exceptions.Handlers;

namespace Productivity.API.Controllers.StatsControllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductivityStatsController : ControllerBase
    {
        private readonly IProductivityStatsService _service;

        public ProductivityStatsController(IProductivityStatsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CollectionDTO<ProductivityStatsModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CollectionDTO<ProductivityStatsModel>>> GetStats([FromQuery] StatsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetStats(query, cancellationToken);
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
