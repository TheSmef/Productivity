using Productivity.API.Data.Repositories.Base;
using Productivity.Shared.Models.Entity;
using System.Linq.Expressions;

namespace Productivity.API.Data.Repositories.Interfaces
{
    public interface ITokenRepository : IRepository<Token>
    {
        public Task<Token?> GetItem(string token, CancellationToken cancellationToken, List<Expression<Func<Token, object>>> expressions);
        public Task<Token?> GetItem(string token, CancellationToken cancellationToken);
    }
}
