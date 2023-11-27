using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public virtual async Task AddItem(TPostDTO record, CancellationToken cancellationToken)
        {
            TEntity item = _mapper.Map<TEntity>(record);
            await _repository.AddItem(item, cancellationToken);
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

        public virtual async Task<CollectionDTO<TDTO>> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            var query = _mapper.ProjectTo<TDTO>(_repository.GetItems(cancellationToken).AsNoTracking());
            CollectionDTO<TDTO> responce = new();
            responce.ElementsCount = DataSpecificationQueryBuilder.GetQueryCount(specification, query);
            responce.Collection = await DataSpecificationQueryBuilder.GetQuery(specification, query)
                .ToListAsync(cancellationToken);
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

        public virtual async Task UpdateItem(Guid Id, TPostDTO record, CancellationToken cancellationToken)
        {
            TEntity item = _mapper.Map<TEntity>(record);
            item.Id = Id;
            await _repository.UpdateItem(item, cancellationToken);
        }
    }
}
