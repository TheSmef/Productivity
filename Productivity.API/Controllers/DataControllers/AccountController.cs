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
using Productivity.Shared.Utility.Exceptions.Handlers;

namespace Productivity.API.Controllers.DataControllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : BaseController<Account, AccountDTO, AccountPostDTO>
    {
        public AccountController(IAccountService service) : base(service) { }
        [HttpPatch]
        public virtual async Task<ActionResult<AccountDTO>> Patch(Guid Id, AccountPatchDTO record,
            CancellationToken cancellationToken)
        {
            var result = await (_service as IAccountService)!.Patch(Id, record, cancellationToken);
            return result.Match<ActionResult<AccountDTO>>(
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
