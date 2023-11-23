using AutoMapper;
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

namespace Productivity.API.Services.Data
{
    public class CultureService : BaseDataService<Culture, CultureDTO, CulturePostDTO>, ICultureService
    {

        public CultureService(ICultureRepository repository, IMapper mapper) : base(repository, mapper) { }

        public override async Task AddItem(CulturePostDTO record, CancellationToken cancellationToken)
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

        public override async Task UpdateItem(Guid Id, CulturePostDTO record, CancellationToken cancellationToken)
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
