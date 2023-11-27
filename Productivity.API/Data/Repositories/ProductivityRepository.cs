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
    public class ProductivityRepository : BaseRepository<Shared.Models.Entity.Productivity>, IProductivityRepository
    {
        public ProductivityRepository(DataContext context) : base(context) { }

        public override async Task<List<string?>> CheckValidate(Shared.Models.Entity.Productivity record, CancellationToken cancellationToken)
        {
            List<string?> result = new();
            if (await _context.Productivities.AnyAsync(x => x.Culture == record.Culture
                && x.Region == record.Region && x.Year == record.Year && x.Id != record.Id,
                cancellationToken))
            {
                result.Add(ContextConstants.ProductivityUNError);
            }
            return result;
        }

        public override List<string?> CheckValidateCollection(Shared.Models.Entity.Productivity record, ICollection<Shared.Models.Entity.Productivity> records)
        {
            List<string?> result = new();
            if (records.Where(x => x.Culture == record.Culture
                && x.Region == record.Region && x.Year == record.Year).Count() > 1)
            {
                result.Add(ContextConstants.ProductivityUNErrorCollection);
            }
            return result;
        }

        public override async Task<Shared.Models.Entity.Productivity> EnsureCreated(Shared.Models.Entity.Productivity record, CancellationToken cancellationToken)
        {
            record = _context.Productivities.FirstOrDefault(x => x.Year == record.Year &&
                    x.Culture.Name == record.Culture.Name && x.Region.Name == record.Region.Name) 
                ?? record;
            if (record.Id == Guid.Empty)
            {
                await _context.Productivities.AddAsync(record, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return record;
        }

        public override async Task Validate(Shared.Models.Entity.Productivity record, CancellationToken cancellationToken)
        {
            if (await _context.Productivities.AnyAsync(x => x.Culture == record.Culture
            && x.Region == record.Region && x.Year == record.Year && x.Id != record.Id,
                   cancellationToken))
            {
                throw new DataException(ContextConstants.ProductivityUNError);
            }
        }
    }
}
