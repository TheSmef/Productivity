using DocumentFormat.OpenXml.Office2010.Excel;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using Productivity.API.Data.Context;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
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

        public virtual async Task<TEntity> AddItem(TEntity record, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Add(record);
            await _context.SaveChangesAsync(cancellationToken);
            return record;
        }

        public virtual async Task AddRange(ICollection<TEntity> records, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().AddRange(records);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public abstract Task<List<string?>> Validate(TEntity record, CancellationToken cancellationToken);

        public abstract List<string?> ValidateCollection(TEntity record, ICollection<TEntity> records);

        public abstract Task<TEntity> EnsureCreated(TEntity record, CancellationToken cancellationToken);

        public async Task<bool> Exists(Guid Id)
        {
            return await _context.Set<TEntity>().AnyAsync(x => x.Id == Id);
        }

        public virtual async Task<TEntity?> GetItem(Guid Id, CancellationToken cancellationToken,
            List<Expression<Func<TEntity, object>>> expressions)
        {
            var items = _context.Set<TEntity>().AsQueryable();
            foreach(var expression in expressions)
            {
                items = items.Include(expression);
            }
            return await items
                 .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public virtual async Task<TEntity?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>()
                 .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<TEntity?> GetItem(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(expression, cancellationToken);
        }

        public virtual IQueryable<TEntity> GetItems(CancellationToken cancellationToken,
            Expression<Func<TEntity, bool>>? expression = null)
        {
            if (expression == null)
            {
                return _context.Set<TEntity>();
            }
            return _context.Set<TEntity>().Where(expression);
        }

        public virtual async Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            var record = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (record != null)
            {
                _context.Set<TEntity>().Remove(record);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task<Result<TEntity>> UpdateItem(TEntity record, CancellationToken cancellationToken)
        {
            if (!_context.Set<TEntity>().Any(x => x.Id == record.Id))
            {
                return new Result<TEntity>(new QueryException(ContextConstants.NotFoundError));
            }
            _context.Set<TEntity>().Update(record);
            await _context.SaveChangesAsync(cancellationToken);
            return record;
        }

        public abstract Task<Result<LanguageExt.Unit>> CanBeDeleted(Guid id, CancellationToken cancellationToken);

        public async Task<TEntity?> GetItemWithoutTracking(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
    }
}
