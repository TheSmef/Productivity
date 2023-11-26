using AutoMapper;
using Productivity.API.Data.Context.Constants;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Security;
using Productivity.Shared.Utility.Exceptions;

namespace Productivity.API.Services.Data
{
    public class AccountService : BaseDataService<Account, AccountDTO, AccountPostDTO>, IAccountService
    {
        public AccountService(IAccountRepository repository, IMapper mapper) : base(repository, mapper) { }

        public async override Task AddItem(AccountPostDTO record, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(record);
            account.Password = HashProvider.MakeHash(account.Password);
            await _repository.AddItem(account, cancellationToken);
        }

        public async override Task UpdateItem(Guid Id, AccountPostDTO record, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(record);
            account.Id = Id;
            if (account.Password == string.Empty)
            {
                var check = await _repository.GetItem(Id, cancellationToken);
                if (check != null)
                {
                    account.Password = check.Password;
                }
            }
            else
            {
                account.Password = HashProvider.MakeHash(account.Password);
            }
            await _repository.UpdateItem(account, cancellationToken);
        }
    }
}
