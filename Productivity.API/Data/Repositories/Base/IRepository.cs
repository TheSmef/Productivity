using Productivity.API.Data.Context;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Data.Repositories.Base
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task<IQueryable<TEntity>> GetItems(QuerySupporter specification);
        public Task<IQueryable<TEntity>> GetItem(Guid Id);
        public Task<int> GetItemsCount(QuerySupporter specification);
        public Task AddItem(TEntity record);
        public Task RemoveItem(Guid Id);
        public Task UpdateItem(TEntity record);
    }
}
