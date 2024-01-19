using LanguageExt;
using Productivity.API.Data.Repositories.Base;
using Productivity.Shared.Models.Entity;

namespace Productivity.API.Data.Repositories.Interfaces
{
    public interface IProductivityRepository : IRepository<Shared.Models.Entity.Productivity>
    {
        public Task<List<string?>> ValidateWithoutParents(Shared.Models.Entity.Productivity record, CancellationToken cancellationToken);
    }
}
