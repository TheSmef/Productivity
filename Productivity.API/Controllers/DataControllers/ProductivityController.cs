using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.DataControllers.Base;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Controllers.DataControllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductivityController : BaseController<Shared.Models.Entity.Productivity, ProductivityDTO, ProductivityPostDTO>
    {
        public ProductivityController(IProductivityService service) : base(service) { }

        [AllowAnonymous]
        [ProducesResponseType(typeof(CollectionDTO<AccountDTO>), StatusCodes.Status200OK)]
        public async override Task<ActionResult<CollectionDTO<ProductivityDTO>>> GetItems([FromQuery] QuerySupporter specification,
            CancellationToken cancellationToken)
        {
            return await base.GetItems(specification, cancellationToken);
        }
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductivityDTO), StatusCodes.Status200OK)]
        public async override Task<ActionResult<ProductivityDTO>> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return await base.GetItem(Id, cancellationToken);
        }

        [ProducesResponseType(typeof(ProductivityDTO), StatusCodes.Status200OK)]
        public async override Task<ActionResult<ProductivityDTO>> PostItem(ProductivityPostDTO record, CancellationToken cancellationToken)
        {
            return await base.PostItem(record, cancellationToken);
        }

        [ProducesResponseType(typeof(ProductivityDTO), StatusCodes.Status200OK)]
        public async override Task<ActionResult<ProductivityDTO>> PutItem(Guid Id, ProductivityPostDTO record, CancellationToken cancellationToken)
        {
            return await base.PutItem(Id, record, cancellationToken);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async override Task<ActionResult> DeleteItem(Guid Id, CancellationToken cancellationToken)
        {
            return await base.DeleteItem(Id, cancellationToken);
        }
    }
}
