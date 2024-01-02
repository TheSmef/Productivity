using LanguageExt;
using LanguageExt.Common;
using Productivity.API.Data.Context;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using System.Linq.Expressions;

namespace Productivity.API.Data.Repositories.Base
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task<TEntity?> GetItem(Guid Id, CancellationToken cancellationToken, List<Expression<Func<TEntity, object>>> expressions);
        public Task<TEntity?> GetItem(Guid Id, CancellationToken cancellationToken);
        public Task<TEntity?> GetItemWithoutTracking(Guid Id, CancellationToken cancellationToken);
        public Task<TEntity> AddItem(TEntity record, CancellationToken cancellationToken);
        public Task AddRange(ICollection<TEntity> records, CancellationToken cancellationToken);
        public Task RemoveItem(Guid Id, CancellationToken cancellationToken);
        public Task<Result<TEntity>> UpdateItem(TEntity record, CancellationToken cancellationToken);
        public Task<TEntity?> GetItem(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        public IQueryable<TEntity> GetItems(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? expression = null);
        public Task<TEntity> EnsureCreated(TEntity record, CancellationToken cancellationToken);
        public Task<bool> Exists(Guid Id);
        public Task<List<string?>> Validate(TEntity record, CancellationToken cancellationToken);
        public List<string?> ValidateCollection(TEntity record, ICollection<TEntity> records);
        public Task<Result<Unit>> CanBeDeleted(Guid id, CancellationToken cancellationToken);
    }
}
