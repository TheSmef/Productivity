using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Security;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.ModelHelpers;
using System.Data;
using System.Linq.Expressions;

namespace Productivity.API.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {

        public AccountRepository(DataContext context) : base(context) {}

        public async Task<Account?> AuthUser(AuthDTO record, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(x => x.Email == record.Login || x.Login == record.Login, cancellationToken);
            if (account != null)
            {
                if (HashProvider.CheckHash(record.Password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }

    }
}
