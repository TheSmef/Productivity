using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Services.Authentication.Base;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels.Base;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Exceptions;

namespace Productivity.API.Controllers.DataControllers.Base
{
    public abstract class BaseController<TEntity, TDTO, TPostDTO> : ControllerBase
        where TEntity : BaseEntity
        where TDTO : BaseDTO
    {
        protected readonly IBaseDataService<TDTO, TPostDTO> _service;

        public BaseController(IBaseDataService<TDTO, TPostDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<CollectionDTO<TDTO>>> GetItems([FromQuery] QuerySupporter specification,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetItems(specification, cancellationToken);
            return Ok(result);
        }


        [HttpGet("{Id:guid}")]
        public virtual async Task<ActionResult<TDTO>> GetItem(Guid Id,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetItem(Id, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<ActionResult> PostItem(TPostDTO record,
            CancellationToken cancellationToken)
        {
            await _service.AddItem(record, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public virtual async Task<ActionResult> PutItem(Guid Id, TPostDTO record,
            CancellationToken cancellationToken)
        {
            await _service.UpdateItem(Id, record, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public virtual async Task<ActionResult> DeleteItem(Guid Id,
            CancellationToken cancellationToken)
        {
            await _service.RemoveItem(Id, cancellationToken);
            return Ok();
        }

    }
}
