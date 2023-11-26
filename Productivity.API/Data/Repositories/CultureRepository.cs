using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Context.Constants;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.ModelHelpers;
using System.Linq.Expressions;

namespace Productivity.API.Data.Repositories
{
    public class CultureRepository : BaseRepository<Culture>, ICultureRepository
    {
        public CultureRepository(DataContext context) : base(context) { }

        public override async Task<List<string?>> CheckValidate(Culture record, CancellationToken cancellationToken)
        {
            List<string> result = new();
            if (await _context.Cultures.AnyAsync(x => x.Name == record.Name && x.Id != record.Id,
                    cancellationToken: cancellationToken))
            {
                result.Add(ContextConstants.CultureUNError);
            }
            return result;
        }

        public override async Task<Culture> EnsureCreated(Culture record, CancellationToken cancellationToken)
        {
            record = _context.Cultures.FirstOrDefault(x => x.Name == record.Name) ?? record;
            if (record.Id == Guid.Empty)
            {
                await _context.Cultures.AddAsync(record, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return record;
        }

        public override async Task Validate(Culture record, CancellationToken cancellationToken)
        {
            if (await _context.Cultures.AnyAsync(x => x.Name == record.Name && x.Id != record.Id,
                    cancellationToken: cancellationToken))
            {
                throw new DataException(ContextConstants.CultureUNError);
            }
        }
    }
}
