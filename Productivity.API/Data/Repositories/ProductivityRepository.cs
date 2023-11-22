using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;
using System.Linq.Expressions;

namespace Productivity.API.Data.Repositories
{
    public class ProductivityRepository : BaseRepository<Shared.Models.Entity.Productivity>, IProductivityRepository
    {
        public ProductivityRepository(DataContext context) : base(context) { }

    }
}
