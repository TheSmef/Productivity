using LanguageExt;
using LanguageExt.Common;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Services.ExportServices.Base
{
    public interface IFileService<TEntity, TExportModel>
        where TEntity : BaseEntity
    {
        public Task<Result<Unit>> ImportItems(Stream stream, CancellationToken cancellationToken);
        public Task<Result<FileModel>> ExportItems(QuerySupporter specification, CancellationToken cancellationToken);
    }
}
