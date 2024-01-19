using LanguageExt.Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;

namespace Productivity.API.Data.Repositories
{
    public class CultureRepository : BaseRepository<Culture>, ICultureRepository
    {
        public CultureRepository(DataContext context) : base(context) { }

        public override async Task<List<string?>> Validate(Culture record, CancellationToken cancellationToken)
        {
            List<string?> result = new();
            if (await _context.Cultures.AnyAsync(x => x.Name == record.Name && x.Id != record.Id,
                    cancellationToken))
            {
                result.Add(ContextConstants.CultureUNError);
            }
            return result;
        }

        public override List<string?> ValidateCollection(Culture record, ICollection<Culture> records)
        {
            List<string?> result = new();
            if (records.Any(x => x.Name == record.Name))
            {
                result.Add(ContextConstants.CultureUNErrorCollection);
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

        public override async Task<Result<Unit>> CanBeDeleted(Guid id, CancellationToken cancellationToken)
        {
            if (await _context.Productivities.Where(x => x.Culture.Id == id).AnyAsync(cancellationToken))
            {
                return new Result<Unit>(new DataException([ ContextConstants.CannotBeDeleted ], ContextConstants.CannotBeDeletedTitle));
            }
            return Unit.Default;
        }
    }
}
