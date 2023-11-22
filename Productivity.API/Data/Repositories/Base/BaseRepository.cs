using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;
using System.Linq.Expressions;

namespace Productivity.API.Data.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddItem(TEntity record, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Add(record);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(ICollection<TEntity> records, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().AddRange(records);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity?> GetItem(Guid Id, CancellationToken cancellationToken,
            Expression<Func<TEntity, object>>? expression = null)
        {
            if (expression == null)
            {
                return await _context.Set<TEntity>()
                    .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            }
            return await _context.Set<TEntity>()
                .Include(expression)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public IQueryable<TEntity> GetItems(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>>? expression = null)
        {
            if (expression == null)
            {
                return _context.Set<TEntity>();
            }
            return _context.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQuery(specification, _context.Set<TEntity>().AsNoTracking());
        }

        public int GetItemsCount(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQueryCount(specification, _context.Set<TEntity>().AsNoTracking());
        }

        public async Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            var record = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (record != null)
            {
                _context.Set<TEntity>().Remove(record);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateItem(TEntity record, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Update(record);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
