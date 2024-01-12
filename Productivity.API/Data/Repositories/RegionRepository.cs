using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.ModelHelpers;
using System.Linq.Expressions;
using System.Threading;

namespace Productivity.API.Data.Repositories
{
    public class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(DataContext context) : base(context) { }

        public override async Task<List<string?>> Validate(Region record, CancellationToken cancellationToken)
        {
            List<string?> result = new();
            if (await _context.Regions.AnyAsync(x => x.Name == record.Name && x.Id != record.Id,
                    cancellationToken))
            {
                result.Add(ContextConstants.RegionUNError);
            }
            return result;
        }

        public override List<string?> ValidateCollection(Region record, ICollection<Region> records)
        {
            List<string?> result = new();
            if (records.Any(x => x.Name == record.Name))
            {
                result.Add(ContextConstants.RegionUNErrorCollection);
            }
            return result;
        }

        public override async Task<Region> EnsureCreated(Region record, CancellationToken cancellationToken)
        {
            record = _context.Regions.FirstOrDefault(x => x.Name == record.Name) ?? record;
            if (record.Id == Guid.Empty)
            {
                await _context.Regions.AddAsync(record, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return record;
        }

        public override async Task<Result<Unit>> CanBeDeleted(Guid id, CancellationToken cancellationToken)
        {
            if (await _context.Productivities.Where(x => x.Region.Id == id).AnyAsync(cancellationToken))
            {
                return new Result<Unit>(new DataException(ContextConstants.CannotBeDeleted));
            }
            return Unit.Default;
        }
    }
}
