using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.FileControllers.Base;
using Productivity.API.Services.ExportServices.Base;
using Productivity.API.Services.ExportServices.Interfaces;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.DTO.File.ExportModels;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Controllers.FileControllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductivityFileController :
        BaseFileController<Shared.Models.Entity.Productivity, ProductivityFileModel>
    {
        public ProductivityFileController(IProductivityFileService service) : base(service) { }
    }
}
