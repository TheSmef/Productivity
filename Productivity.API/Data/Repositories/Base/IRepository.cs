using Productivity.API.Data.Context;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using System.Linq.Expressions;

namespace Productivity.API.Data.Repositories.Base
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public IQueryable<TEntity> GetItems(QuerySupporter specification, CancellationToken cancellationToken);
        public Task<TEntity?> GetItem(Guid Id, CancellationToken cancellationToken, Expression<Func<TEntity, object>>? expression = null);
        public int GetItemsCount(QuerySupporter specification, CancellationToken cancellationToken);
        public Task AddItem(TEntity record, CancellationToken cancellationToken);
        public Task AddRange(ICollection<TEntity> records, CancellationToken cancellationToken);
        public Task RemoveItem(Guid Id, CancellationToken cancellationToken);
        public Task UpdateItem(TEntity record, CancellationToken cancellationToken);
        public IQueryable<TEntity> GetItems(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? expression = null);
    }
}
