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
    }
}
