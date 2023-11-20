using Productivity.API.Data.Repositories.Base;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;
using Productivity.Shared.Models.Entity;

namespace Productivity.API.Data.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Task<Account?> AuthUser(AuthDTO record, CancellationToken cancellationToken);
    }
}
