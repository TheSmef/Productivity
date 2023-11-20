using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;

namespace Productivity.API.Data.Repositories
{
    public class ProductivityRepository : IProductivityRepository
    {
        private readonly DataContext _context;

        public ProductivityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddItem(Shared.Models.Entity.Productivity record, CancellationToken cancellationToken)
        {
            _context.Productivities.Add(record);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(ICollection<Shared.Models.Entity.Productivity> records, CancellationToken cancellationToken)
        {
            _context.Productivities.AddRange(records);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Shared.Models.Entity.Productivity?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Productivities
                .Include(x => x.Culture)
                .Include(x => x.Region)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public IQueryable<Shared.Models.Entity.Productivity> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQuery(specification, _context.Productivities);
        }

        public int GetItemsCount(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQueryCount(specification, _context.Productivities);
        }

        public async Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            var record = await _context.Productivities
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (record != null)
            {
                _context.Productivities.Remove(record);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateItem(Shared.Models.Entity.Productivity record, CancellationToken cancellationToken)
        {
            _context.Productivities.Update(record);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
