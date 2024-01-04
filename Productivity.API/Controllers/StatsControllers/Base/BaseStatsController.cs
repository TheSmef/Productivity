using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Stats.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Exceptions.Handlers;
using Productivity.API.Services.Stats.Base;
using Productivity.Shared.Models.Utility.Base;

namespace Productivity.API.Controllers.StatsControllers.Base
{
    public abstract class BaseStatsController<T, TDTO> : ControllerBase
        where T : StatsQuery
        where TDTO : class
    {
        private readonly IStatsService<T, TDTO> _service;

        public BaseStatsController(IStatsService<T, TDTO> service)
        {
            _service = service;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<CollectionDTO<TDTO>>> GetStats([FromQuery] T query,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetStats(query, cancellationToken);
            return result.Match<ActionResult<CollectionDTO<TDTO>>>(
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
