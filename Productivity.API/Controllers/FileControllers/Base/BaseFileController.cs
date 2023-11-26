using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.ExportServices.Base;
using Productivity.API.Services.ExportServices.Interfaces;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;

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
        public async Task<ActionResult<FileModel>> Export([FromQuery] QuerySupporter specification,
            CancellationToken cancellationToken)
        {
            FileModel responce = await _service.ExportItems(specification, cancellationToken);
            return Ok(responce);
        }

        [HttpPost]
        public async Task<ActionResult> Import(byte[] bytes, CancellationToken cancellationToken)
        {
            await _service.ImportItems(bytes, cancellationToken);
            return Ok();
        }
    }
}
