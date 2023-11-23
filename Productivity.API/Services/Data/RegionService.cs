using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context.Constants;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Data.Base;
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
    public class RegionService : BaseDataService<Region, RegionDTO, RegionPostDTO>, IRegionService
    {

        public RegionService(IRegionRepository repository, IMapper mapper) : base(repository, mapper) { }

        public override async Task AddItem(RegionPostDTO record, CancellationToken cancellationToken)
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

        public override async Task UpdateItem(Guid Id, RegionPostDTO record, CancellationToken cancellationToken)
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
