using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using LanguageExt.Common;
using Microsoft.IdentityModel.Tokens;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Security;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;

namespace Productivity.API.Services.Data
{
    public class AccountService : BaseDataService<Account, AccountDTO, AccountPostDTO>, IAccountService
    {
        public AccountService(IAccountRepository repository, IMapper mapper) : base(repository, mapper) { }

        public async override Task<Result<AccountDTO>> AddItem(AccountPostDTO record, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(record);
            account.Password = HashProvider.MakeHash(account.Password);
            var result = await _repository.Validate(account, cancellationToken);
            if (!result.IsNullOrEmpty())
            {
                return new Result<AccountDTO>(new DataException(result, ContextConstants.ValidationErrorTitle));
            }
            account = await _repository.AddItem(account, cancellationToken);
            AccountDTO item = _mapper.Map<AccountDTO>(account);
            return item;
        }

        public async Task<Result<AccountDTO>> Patch(Guid Id, AccountPatchDTO record, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(record);
            account.Id = Id;
            var check = await _repository.GetItemWithoutTracking(Id, cancellationToken);
            if (check != null)
            {
               account.Password = check.Password;
            }
            var result = await _repository.Validate(account, cancellationToken);
            if (!result.IsNullOrEmpty())
            {
                return new Result<AccountDTO>(new DataException(result, ContextConstants.ValidationErrorTitle));
            }
            var responce = await _repository.UpdateItem(account, cancellationToken);
            return responce.Match(
                succ => _mapper.Map<AccountDTO>(succ),
                err => new Result<AccountDTO>(err));
        }

        public async override Task<Result<AccountDTO>> UpdateItem(Guid Id, AccountPostDTO record, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(record);
            account.Id = Id;
            account.Password = HashProvider.MakeHash(account.Password);
            var result = await _repository.Validate(account, cancellationToken);
            if (!result.IsNullOrEmpty())
            {
                return new Result<AccountDTO>(new DataException(result, ContextConstants.ValidationErrorTitle));
            }
            var responce = await _repository.UpdateItem(account, cancellationToken);
            return responce.Match(
                succ => _mapper.Map<AccountDTO>(succ),
                err => new Result<AccountDTO>(err));
        }
    }
}
