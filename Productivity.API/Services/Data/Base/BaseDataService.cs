using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels.Base;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.ModelHelpers;
using System.Linq.Dynamic.Core.Exceptions;

namespace Productivity.API.Services.Data.Base
{
    public abstract class BaseDataService<TEntity, TDTO, TPostDTO> : IBaseDataService<TDTO, TPostDTO>
        where TEntity : BaseEntity
        where TDTO : BaseDTO
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseDataService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<Result<TDTO>> AddItem(TPostDTO record, CancellationToken cancellationToken)
        {
            TEntity item = _mapper.Map<TEntity>(record);
            var result = await _repository.Validate(item, cancellationToken);
            if (!result.IsNullOrEmpty())
            {
                return new Result<TDTO>(new DataException(result, ContextConstants.ValidationErrorTitle));
            }
            item = await _repository.AddItem(item, cancellationToken);
            return _mapper.Map<TDTO>(item);
        }

        public virtual async Task<TDTO?> GetItem(Guid Id, CancellationToken cancellationToken)
        {     
            var item = await _repository.GetItem(Id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            TDTO record = _mapper.Map<TDTO>(item);
            return record;
        }

        public virtual Task<Result<CollectionDTO<TDTO>>> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            var query = _mapper.ProjectTo<TDTO>(_repository.GetItems(cancellationToken).AsNoTracking());
            return ResponceModelBuilder.Build(specification, query, cancellationToken);
        }

        public async Task<Result<Unit>> RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            var result = await _repository.CanBeDeleted(Id, cancellationToken);
            if (result.IsFaulted)
            {
                return result;
            }
            await _repository.RemoveItem(Id, cancellationToken);
            return Unit.Default;
        }

        public virtual async Task<Result<TDTO>> UpdateItem(Guid Id, TPostDTO record, CancellationToken cancellationToken)
        {
            TEntity item = _mapper.Map<TEntity>(record);
            item.Id = Id;
            var result = await _repository.Validate(item, cancellationToken);
            if (!result.IsNullOrEmpty())
            {
                return new Result<TDTO>(new DataException(result, ContextConstants.ValidationErrorTitle));
            }
            var responce = await _repository.UpdateItem(item, cancellationToken);
            return responce.Match(
                succ => _mapper.Map<TDTO>(succ), 
                err => new Result<TDTO>(err));
        }
    }
}
