using Productivity.MailService.Data.Context;
using Productivity.MailService.Data.Repositories.Base;
using Productivity.MailService.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Data.Repositories
{
    public class MailRepository : BaseRepository<Mail>, IMailRepository
    {
        public MailRepository(DataContext dataContext) : base(dataContext) { }
    }
}
