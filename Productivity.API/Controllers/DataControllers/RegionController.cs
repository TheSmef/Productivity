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
    public class RegionController : BaseController<Region, RegionDTO, RegionPostDTO>
    {
        public RegionController(IRegionService repository) : base(repository) { }
        [AllowAnonymous]
        public async override Task<ActionResult<CollectionDTO<RegionDTO>>> GetItems([FromQuery] QuerySupporter specification,
            CancellationToken cancellationToken)
        {
            return await base.GetItems(specification, cancellationToken);
        }
    }
}
