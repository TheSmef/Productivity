using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;
using System.Linq.Expressions;
using System.Threading;

namespace Productivity.API.Data.Repositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(DataContext context) : base(context) { }

        public async Task<Token?> GetItem<T>(string token, CancellationToken cancellationToken,
            Expression<Func<Token, T>>? expression = null)
        {
            if (expression == null)
            {
                return await _context.Tokens.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.TokenStr == token, cancellationToken);
            }
            return await _context.Tokens.Include(expression).AsNoTracking()
                .FirstOrDefaultAsync(x => x.TokenStr == token, cancellationToken);
        }
    }
}
