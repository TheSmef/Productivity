using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context.Constants;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.ModelHelpers;

namespace Productivity.API.Services.Data
{
    public class CultureService : ICultureService
    {
        private readonly ICultureRepository _repository;
        private readonly IMapper _mapper;

        public CultureService(ICultureRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddItem(CulturePostDTO record, CancellationToken cancellationToken)
        {
            try
            {
                Culture culture = _mapper.Map<Culture>(record);
                await _repository.AddItem(culture, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.Message.Contains(ContextConstants.CultureUNIndex))
                        throw new QueryException("Данное название культуры уже занято");
                throw;
            }

        }

        public async Task<CultureDTO?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            var culture = await _repository.GetItem(Id, cancellationToken);
            if (culture == null)
            {
                return null;
            }
            CultureDTO record = _mapper.Map<CultureDTO>(culture);
            return record;
        }

        public async Task<CollectionDTO<CultureDTO>> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            var query = _repository.GetItems(specification, cancellationToken);
            CollectionDTO<CultureDTO> responce = new();
            responce.Collection = await _mapper.ProjectTo<CultureDTO>(query).ToListAsync(cancellationToken);
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

        public async Task UpdateItem(Guid Id, CulturePostDTO record, CancellationToken cancellationToken)
        {
            try
            {
                Culture culture = _mapper.Map<Culture>(record);
                culture.Id = Id;
                await _repository.UpdateItem(culture, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.Message.Contains(ContextConstants.CultureUNIndex))
                        throw new QueryException("Данное название культуры уже занято");
                throw;
            }
        }
    }
}
