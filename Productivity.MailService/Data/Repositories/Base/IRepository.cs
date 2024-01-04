using Productivity.Shared.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Data.Repositories.Base
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task<TEntity> AddItem(TEntity entity, CancellationToken cancellationToken);
    }
}
