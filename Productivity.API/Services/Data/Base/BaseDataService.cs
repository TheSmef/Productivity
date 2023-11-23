using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context.Constants;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels.Base;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Entity.Base;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.ModelHelpers;

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

        public abstract Task AddItem(TPostDTO record, CancellationToken cancellationToken);

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

        public async Task<CollectionDTO<TDTO>> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            var query = _repository.GetItems(specification, cancellationToken);
            CollectionDTO<TDTO> responce = new();
            responce.Collection = await _mapper.ProjectTo<TDTO>(query).ToListAsync(cancellationToken);
            responce.ElementsCount = _repository.GetItemsCount(specification, cancellationToken);
            responce.TotalPages = specification.Top == 0 ? 0 : PageCounter.CountPages(responce.ElementsCount, specification.Top);
            responce.CurrentPageIndex = specification.Top == 0 ? 0 : PageCounter.CountCurrentPage(
                responce.TotalPages,
                responce.ElementsCount,
                specification.Skip,
                specification.Top
                );
            return responce;
        }

        public async Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            await _repository.RemoveItem(Id, cancellationToken);
        }

        public abstract Task UpdateItem(Guid Id, TPostDTO record, CancellationToken cancellationToken);
    }
}
