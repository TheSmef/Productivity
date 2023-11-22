using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Spreadsheet;
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
using System.Reflection.Metadata.Ecma335;

namespace Productivity.API.Services.Data
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _repository;
        private readonly IMapper _mapper;

        public RegionService(IRegionRepository regionRepository, IMapper mapper)
        {
            _repository = regionRepository;
            _mapper = mapper;
        }

        public async Task AddItem(RegionPostDTO record, CancellationToken cancellationToken)
        {
            try
            {
                Region region = _mapper.Map<Region>(record);
                await _repository.AddItem(region, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.Message.Contains(ContextConstants.RegionUNIndex))
                        throw new QueryException("Данное название региона уже занято");
                throw;
            }

        }

        public async Task<RegionDTO?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            var region = await _repository.GetItem(Id, cancellationToken);
            if (region == null)
            {
                return null;
            }
            RegionDTO record = _mapper.Map<RegionDTO>(region);
            return record;
        }

        public async Task<CollectionDTO<RegionDTO>> GetItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            var query = _repository.GetItems(specification, cancellationToken);
            CollectionDTO<RegionDTO> responce = new();
            responce.Collection = await _mapper.ProjectTo<RegionDTO>(query).ToListAsync(cancellationToken);
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

        public async Task UpdateItem(Guid Id, RegionPostDTO record, CancellationToken cancellationToken)
        {
            try
            {
                Region region = _mapper.Map<Region>(record);
                region.Id = Id;
                await _repository.UpdateItem(region, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.Message.Contains(ContextConstants.RegionUNIndex))
                        throw new QueryException("Данное название региона уже занято");
                throw;
            }
        }
    }
}
