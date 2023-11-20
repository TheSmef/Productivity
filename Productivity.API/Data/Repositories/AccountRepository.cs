using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Security;
using Productivity.Shared.Utility.ModelHelpers;
using System.Data;

namespace Productivity.API.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddItem(Account record, CancellationToken cancellationToken)
        {
            _context.Accounts.Add(record);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(ICollection<Account> records, CancellationToken cancellationToken)
        {
            _context.Accounts.AddRange(records);
            await _context.SaveChangesAsync(cancellationToken);
        }

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

        public async Task<Account?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public IQueryable<Account> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQuery(specification, _context.Accounts);
        }

        public int GetItemsCount(QuerySupporter specification, CancellationToken cancellationToken)
        {
            return DataSpecificationQueryBuilder.GetQueryCount(specification, _context.Accounts);
        }

        public async Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            var record = await _context.Accounts
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (record != null)
            {
                _context.Accounts.Remove(record);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateItem(Account record, CancellationToken cancellationToken)
        {
            _context.Accounts.Update(record);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
