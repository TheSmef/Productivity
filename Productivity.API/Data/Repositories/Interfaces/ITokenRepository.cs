using Productivity.API.Data.Repositories.Base;
using Productivity.Shared.Models.Entity;

namespace Productivity.API.Data.Repositories.Interfaces
{
    public interface ITokenRepository : IRepository<Token>
    {
        public Task<Token?> GetItem(string token, CancellationToken cancellationToken);
    }
}
