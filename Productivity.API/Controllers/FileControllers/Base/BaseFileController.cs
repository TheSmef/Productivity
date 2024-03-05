using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.ExportServices.Base;
using Productivity.API.Services.ExportServices.Interfaces;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.DTO.FileModels.ImportModels;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.ErrorModels;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions.Handlers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Productivity.API.Controllers.FileControllers.Base
{
    public abstract class BaseFileController<TEntity, TFileModel> : ControllerBase
        where TEntity : BaseEntity
    {
        protected readonly IFileService<TEntity, TFileModel> _service;
        protected readonly IOutputCacheStore _store;
        protected readonly List<string>? _evictingTags;

        public BaseFileController(IFileService<TEntity, TFileModel> service, 
            IOutputCacheStore store, List<string>? evictingTags = null)
        {
            _service = service;
            _store = store;
            _evictingTags = evictingTags;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Export([FromQuery] QuerySupporter specification,
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
        public virtual async Task<ActionResult> Import(ImportFileModel model, CancellationToken cancellationToken)
        {
            var result = await _service.ImportItems(model.File!.OpenReadStream(), cancellationToken);
            return result.Match<ActionResult>(
                succ =>
                {
                    _evictingTags?.ForEach(async x => await _store.EvictByTagAsync(x, cancellationToken));
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
