using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Security;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.ModelHelpers;
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

        public override async Task<List<string?>> Validate(Account record, CancellationToken cancellationToken)
        {
            List<string?> result = new();
            if (await _context.Accounts.AnyAsync(x => x.Email == record.Email && x.Id != record.Id,
                cancellationToken))
            {
                result.Add(ContextConstants.AccountUNEmailError);
            }
            if (await _context.Accounts.AnyAsync(x => x.Login == record.Login && x.Id != record.Id, 
                cancellationToken))
            {
                result.Add(ContextConstants.AccountUNLoginError);
            }
            return result;
        }

        public override List<string?> ValidateCollection(Account record, ICollection<Account> records)
        {
            List<string?> result = new();
            if (records.Any(x => x.Email == record.Email))
            {
                result.Add(ContextConstants.AccountUNEmailErrorCollection);
            }
            if (records.Any(x => x.Login == record.Login))
            {
                result.Add(ContextConstants.AccountUNLoginErrorCollection);
            }
            return result;
        }

        public override async Task<Account> EnsureCreated(Account record, CancellationToken cancellationToken)
        {
            record = _context.Accounts.FirstOrDefault(x => x.Login == record.Login) ?? record;
            if (record.Id == Guid.Empty)
            {
                await _context.Accounts.AddAsync(record, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return record;
        }

        public override async Task<Result<Account>> UpdateItem(Account record, CancellationToken cancellationToken)
        {
            _context.Tokens.RemoveRange(_context.Tokens.Where(x => x.Account.Id == record.Id));
            return await base.UpdateItem(record, cancellationToken);
        }
    }
}
