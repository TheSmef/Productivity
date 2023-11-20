using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;

namespace Productivity.API.Data.Repositories
{
    public class CultureRepository : ICultureRepository
    {
        private readonly DataContext _context;

        public CultureRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddItem(Culture record, CancellationToken cancellationToken)
        {
            _context.Cultures.Add(record);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(ICollection<Culture> records, CancellationToken cancellationToken)
        {
            _context.Cultures.AddRange(records);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Culture?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Cultures
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public IQueryable<Culture> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQuery(specification, _context.Cultures);
        }

        public int GetItemsCount(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQueryCount(specification, _context.Cultures);
        }

        public async Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            var record = await _context.Cultures
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (record != null)
            {
                _context.Cultures.Remove(record);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateItem(Culture record, CancellationToken cancellationToken)
        {
            _context.Cultures.Update(record);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
