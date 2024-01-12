using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.ExportServices.Base;
using Productivity.API.Services.ExportServices.Interfaces;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions.Handlers;

namespace Productivity.API.Controllers.FileControllers.Base
{
    public abstract class BaseFileController<TEntity, TFileModel> : ControllerBase
        where TEntity : BaseEntity
    {
        private readonly IFileService<TEntity, TFileModel> _service;

        public BaseFileController(IFileService<TEntity, TFileModel> service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Export([FromQuery] QuerySupporter specification,
            CancellationToken cancellationToken)
        {
            var result = await _service.ExportItems(specification, cancellationToken);
            return result.Match<ActionResult>(
                succ =>
                {
                    return File(succ.Data, ContextConstants.ExcelContentType, succ.Name);
                },
                err =>
                {
                    return BadRequest(ExceptionMapper.Map(err));
                }
                );
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Import(IFormFile file, CancellationToken cancellationToken)
        {
            var result = await _service.ImportItems(file.OpenReadStream(), cancellationToken);
            return result.Match<ActionResult>(
                succ =>
                {
                    return Ok();
                },
                err =>
                {
                    return BadRequest(ExceptionMapper.Map(err));
                }
                );
        }
    }
}
