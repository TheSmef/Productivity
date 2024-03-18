using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.FileControllers.Base;
using Productivity.API.Services.ExportServices.Base;
using Productivity.API.Services.ExportServices.Interfaces;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.DTO.File.ExportModels;
using Productivity.Shared.Models.DTO.FileModels.ImportModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Constants;

namespace Productivity.API.Controllers.FileControllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductivityFileController :
        BaseFileController<Shared.Models.Entity.Productivity, ProductivityFileModel>
    {
        public ProductivityFileController(IProductivityFileService service) 
            : base(service) { }

        public override Task<ActionResult> Export([FromQuery] QuerySupporter specification, CancellationToken cancellationToken)
        {
            return base.Export(specification, cancellationToken);
        }
    }
}
