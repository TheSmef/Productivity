using Productivity.API.Services.ExportServices.Base;
using Productivity.Shared.Models.DTO.File.ExportModels;

namespace Productivity.API.Services.ExportServices.Interfaces
{
    public interface IProductivityFileService : 
        IFileService<Shared.Models.Entity.Productivity, ProductivityFileModel> { }
}
