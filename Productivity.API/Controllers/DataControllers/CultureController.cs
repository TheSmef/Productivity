using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.DataControllers.Base;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.ErrorModels;

namespace Productivity.API.Controllers.DataControllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CultureController : BaseController<Culture, CultureDTO, CulturePostDTO>
    {
        public CultureController(ICultureService service) : base(service) { }

        [AllowAnonymous]
        [ProducesResponseType(typeof(CollectionDTO<CultureDTO>), StatusCodes.Status200OK)]
        public async override Task<ActionResult<CollectionDTO<CultureDTO>>> GetItems([FromQuery] QuerySupporter specification,
            CancellationToken cancellationToken)
        {
            return await base.GetItems(specification, cancellationToken);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(CultureDTO), StatusCodes.Status200OK)]
        public async override Task<ActionResult<CultureDTO>> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return await base.GetItem(Id, cancellationToken);
        }

        [ProducesResponseType(typeof(CultureDTO), StatusCodes.Status200OK)]
        public async override Task<ActionResult<CultureDTO>> PostItem(CulturePostDTO record, CancellationToken cancellationToken)
        {
            return await base.PostItem(record, cancellationToken);
        }

        [ProducesResponseType(typeof(CultureDTO), StatusCodes.Status200OK)]
        public async override Task<ActionResult<CultureDTO>> PutItem(Guid Id, CulturePostDTO record, CancellationToken cancellationToken)
        {
            return await base.PutItem(Id, record, cancellationToken);
        }
    }
}
