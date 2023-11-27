using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

namespace Productivity.API.Services.Data
{
    public class CultureService : BaseDataService<Culture, CultureDTO, CulturePostDTO>, ICultureService
    {
        public CultureService(ICultureRepository repository, IMapper mapper) : base(repository, mapper) { }
    }
}
