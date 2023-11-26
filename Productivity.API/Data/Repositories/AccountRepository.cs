using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Context.Constants;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Security;
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

        public override async Task<List<string?>> CheckValidate(Account record, CancellationToken cancellationToken)
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

        public override List<string?> CheckValidateCollection(Account record, ICollection<Account> records)
        {
            List<string?> result = new();
            if (records.Where(x => x.Email == record.Email).Count() > 1)
            {
                result.Add(ContextConstants.AccountUNEmailErrorCollection);
            }
            if (records.Where(x => x.Login == record.Login).Count() > 1)
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

        public override async Task Validate(Account record, CancellationToken cancellationToken)
        {
            if (await _context.Accounts.AnyAsync(x => x.Email == record.Email && x.Id != record.Id,
                cancellationToken))
            {
                throw new DataException(ContextConstants.AccountUNEmailError);
            }
            if (await _context.Accounts.AnyAsync(x => x.Login == record.Login && x.Id != record.Id,
                cancellationToken))
            {
                throw new DataException(ContextConstants.AccountUNLoginError);
            }
        }
    }
}
