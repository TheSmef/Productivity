﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.DataControllers.Base;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Exceptions.Handlers;

namespace Productivity.API.Controllers.DataControllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : BaseController<AccountDTO, AccountPostDTO>
    {
        public AccountController(IAccountService service) 
            : base(service) { }

        [HttpPatch]
        [ProducesResponseType(typeof(AccountDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [ProducesResponseType(typeof(AccountDTO), StatusCodes.Status200OK)]
        public override Task<ActionResult<AccountDTO>> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return base.GetItem(Id, cancellationToken);
        }

        [ProducesResponseType(typeof(CollectionDTO<AccountDTO>), StatusCodes.Status200OK)]
        public override Task<ActionResult<CollectionDTO<AccountDTO>>> GetItems([FromQuery] QuerySupporter specification,
            CancellationToken cancellationToken)
        {
            return base.GetItems(specification, cancellationToken);
        }

        [ProducesResponseType(typeof(AccountDTO), StatusCodes.Status200OK)]
        public override Task<ActionResult<AccountDTO>> PostItem(AccountPostDTO record, CancellationToken cancellationToken)
        {
            return base.PostItem(record, cancellationToken);
        }

        [ProducesResponseType(typeof(AccountDTO), StatusCodes.Status200OK)]
        public override Task<ActionResult<AccountDTO>> PutItem(Guid Id, AccountPostDTO record, CancellationToken cancellationToken)
        {
            return base.PutItem(Id, record, cancellationToken);
        }
    }
}
