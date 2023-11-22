using Productivity.API.Data.Repositories.Base;
using Productivity.Shared.Models.Entity;
using System.Linq.Expressions;

namespace Productivity.API.Data.Repositories.Interfaces
{
    public interface ITokenRepository : IRepository<Token>
    {
        public Task<Token?> GetItem<T>(string token, CancellationToken cancellationToken, Expression<Func<Token, T>>? expression = null);
    }
}
