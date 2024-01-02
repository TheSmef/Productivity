using AutoMapper;
using LanguageExt.Common;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Utility.Constants;
using Productivity.Shared.Utility.Exceptions;

namespace Productivity.API.Services.Data
{
    public class ProductivityService : BaseDataService<Shared.Models.Entity.Productivity,
        ProductivityDTO, ProductivityPostDTO>, IProductivityService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICultureRepository _cultureRepository;
        public ProductivityService(IRegionRepository regionRepository, ICultureRepository cultureRepository,
            IProductivityRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _regionRepository = regionRepository;
            _cultureRepository = cultureRepository;
        }

        public override async Task<Result<ProductivityDTO>> AddItem(ProductivityPostDTO record, CancellationToken cancellationToken)
        {
            Shared.Models.Entity.Productivity item = _mapper.Map<Shared.Models.Entity.Productivity>(record);
            var culture = await _cultureRepository.GetItem(item.Culture.Id, cancellationToken);
            if (culture == null)
            {
                return new Result<ProductivityDTO>(new QueryException(ContextConstants.CultureNotFound));
            }
            item.Culture = culture;
            var region = await _regionRepository.GetItem(item.Region.Id, cancellationToken);
            if (region == null)
            {
                return new Result<ProductivityDTO>(new QueryException(ContextConstants.RegionNotFound));
            }
            item.Region = region;
            var result = await _repository.Validate(item, cancellationToken);
            if (!result.IsNullOrEmpty())
            {
                return new Result<ProductivityDTO>(new DataException(result, ContextConstants.ValidationErrorTitle));
            }
            item = await _repository.AddItem(item, cancellationToken);
            return _mapper.Map<ProductivityDTO>(item);
        }

        public override async Task<ProductivityDTO?> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            var item = await _repository.GetItem(Id, cancellationToken, 
                new() {x => x.Culture, x => x.Region });
            if (item == null)
            {
                return null;
            }
            ProductivityDTO record = _mapper.Map<ProductivityDTO>(item);
            return record;
        }

        public override async Task<Result<ProductivityDTO>> UpdateItem(Guid Id, ProductivityPostDTO record, CancellationToken cancellationToken)
        {
            Shared.Models.Entity.Productivity item = _mapper.Map<Shared.Models.Entity.Productivity>(record);
            var culture = await _cultureRepository.GetItem(item.Culture.Id, cancellationToken);
            if (culture == null)
            {
                return new Result<ProductivityDTO>(new QueryException(ContextConstants.CultureNotFound));
            }
            item.Culture = culture;
            var region = await _regionRepository.GetItem(item.Region.Id, cancellationToken);
            if (region == null)
            {
                return new Result<ProductivityDTO>(new QueryException(ContextConstants.RegionNotFound));
            }
            item.Region = region;
            item.Id = Id;
            var result = await _repository.Validate(item, cancellationToken);
            if (!result.IsNullOrEmpty())
            {
                return new Result<ProductivityDTO>(new DataException(result, ContextConstants.ValidationErrorTitle));
            }
            var responce = await _repository.UpdateItem(item, cancellationToken);
            return responce.Match(
                succ => _mapper.Map<ProductivityDTO>(succ),
                err => new Result<ProductivityDTO>(err));
        }
    }
}
