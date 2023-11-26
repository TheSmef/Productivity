using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Services.ExportServices.Base
{
    public interface IFileService<TEntity, TExportModel>
        where TEntity : BaseEntity
    {
        public Task ImportItems(byte[] bytes, CancellationToken cancellationToken);
        public Task<FileModel> ExportItems(QuerySupporter specification, CancellationToken cancellationToken);
    }
}
