using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.StatsControllers.Base;
using Productivity.API.Services.Stats.Interfaces;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions.Handlers;

namespace Productivity.API.Controllers.StatsControllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductivityStatsController : BaseStatsController<StatsQuery, ProductivityStatsModel>
    {
        public ProductivityStatsController(IProductivityStatsService service) : base(service) { }

        [ProducesResponseType(typeof(CollectionDTO<ProductivityStatsModel>), StatusCodes.Status200OK)]
        public override Task<ActionResult<CollectionDTO<ProductivityStatsModel>>> GetStats([FromQuery] StatsQuery query, CancellationToken cancellationToken)
        {
            return base.GetStats(query, cancellationToken);
        }
    }
}
