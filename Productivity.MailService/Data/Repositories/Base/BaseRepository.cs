using Productivity.MailService.Data.Context;
using Productivity.Shared.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Data.Repositories.Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity> 
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
    }
}
