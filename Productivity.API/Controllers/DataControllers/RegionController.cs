using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.DataControllers.Base;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Controllers.DataControllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RegionController : BaseController<RegionDTO, RegionPostDTO>
    {
        public RegionController(IRegionService repository) : base(repository) { }

        [AllowAnonymous]
        [ProducesResponseType(typeof(CollectionDTO<RegionDTO>), StatusCodes.Status200OK)]
        public override Task<ActionResult<CollectionDTO<RegionDTO>>> GetItems([FromQuery] QuerySupporter specification,
            CancellationToken cancellationToken)
        {
            return base.GetItems(specification, cancellationToken);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(RegionDTO), StatusCodes.Status200OK)]
        public override Task<ActionResult<RegionDTO>> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return base.GetItem(Id, cancellationToken);
        }

        [ProducesResponseType(typeof(RegionDTO), StatusCodes.Status200OK)]
        public override Task<ActionResult<RegionDTO>> PostItem(RegionPostDTO record, CancellationToken cancellationToken)
        {
            return base.PostItem(record, cancellationToken);
        }

        [ProducesResponseType(typeof(RegionDTO), StatusCodes.Status200OK)]
        public override Task<ActionResult<RegionDTO>> PutItem(Guid Id, RegionPostDTO record, CancellationToken cancellationToken)
        {
            return base.PutItem(Id, record, cancellationToken);
        }
    }
}
