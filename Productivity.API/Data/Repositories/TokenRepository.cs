using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;
using System.Threading;

namespace Productivity.API.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DataContext _context;

        public TokenRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddItem(Token record, CancellationToken cancellationToken)
        {
            _context.Tokens.Add(record);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(ICollection<Token> records, CancellationToken cancellationToken)
        {
            _context.Tokens.AddRange(records);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Token?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Tokens.Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<Token?> GetItem(string token, CancellationToken cancellationToken)
        {
            return await _context.Tokens.Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.TokenStr == token, cancellationToken);
        }

        public IQueryable<Token> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQuery(specification, _context.Tokens);
        }

        public int GetItemsCount(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQueryCount(specification, _context.Tokens);
        }

        public async Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            var record = await _context.Tokens
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (record != null)
            {
                _context.Tokens.Remove(record);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateItem(Token record, CancellationToken cancellationToken)
        {
            _context.Tokens.Update(record);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
