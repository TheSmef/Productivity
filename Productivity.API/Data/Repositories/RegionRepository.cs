using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;

namespace Productivity.API.Data.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DataContext _context;

        public RegionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddItem(Region record, CancellationToken cancellationToken)
        {
            _context.Regions.Add(record);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(ICollection<Region> records, CancellationToken cancellationToken)
        {
            _context.Regions.AddRange(records);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Region?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Regions
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public IQueryable<Region> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQuery(specification, _context.Regions);
        }

        public int GetItemsCount(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQueryCount(specification, _context.Regions);
        }

        public async Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            var record = await _context.Regions
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (record != null)
            {
                _context.Regions.Remove(record);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateItem(Region record, CancellationToken cancellationToken)
        {
            _context.Regions.Update(record);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
