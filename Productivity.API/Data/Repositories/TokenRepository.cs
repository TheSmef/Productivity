using DocumentFormat.OpenXml.Office2010.Excel;
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
using System.Threading;

namespace Productivity.API.Data.Repositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(DataContext context) : base(context) { }

        public override async Task<List<string?>> CheckValidate(Token record, CancellationToken cancellationToken)
        {
            List<string> result = new();
            if (await _context.Tokens.AnyAsync(x => x.TokenStr == record.TokenStr && x.Id != record.Id,
                cancellationToken: cancellationToken))
            {
                result.Add(ContextConstants.TokenUNIndex);
            }
            return result;
        }

        public override async Task<Token> EnsureCreated(Token record, CancellationToken cancellationToken)
        {
            record = _context.Tokens.FirstOrDefault(x => x.TokenStr == record.TokenStr) ?? record;
            if (record.Id == Guid.Empty)
            {
                await _context.Tokens.AddAsync(record, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return record;
        }

        public async Task<Token?> GetItem(string token, CancellationToken cancellationToken,
            List<Expression<Func<Token, object>>> expressions)
        {
            var items = _context.Tokens.AsQueryable();
            foreach (var expression in expressions)
            {
                items = items.Include(expression);
            }
            return await items.AsNoTracking()
                .FirstOrDefaultAsync(x => x.TokenStr == token, cancellationToken);
        }

        public async Task<Token?> GetItem(string token, CancellationToken cancellationToken)
        {
            return await _context.Tokens.AsNoTracking()
                .FirstOrDefaultAsync(x => x.TokenStr == token, cancellationToken);
        }

        public override async Task Validate(Token record, CancellationToken cancellationToken)
        {
            if (await _context.Tokens.AnyAsync(x => x.TokenStr == record.TokenStr && x.Id != record.Id,
                    cancellationToken: cancellationToken))
            {
                throw new DataException(ContextConstants.TokenUNIndex);
            }
        }
    }
}
