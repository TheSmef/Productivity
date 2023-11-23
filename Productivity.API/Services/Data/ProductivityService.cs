using AutoMapper;
using Productivity.API.Data.Context.Constants;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Data.Base;
using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Entity;
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

        public override async Task AddItem(ProductivityPostDTO record, CancellationToken cancellationToken)
        {
            try
            {
                Shared.Models.Entity.Productivity item = _mapper.Map<Shared.Models.Entity.Productivity>(record);
                var culture = await _cultureRepository.GetItem(item.Culture.Id, cancellationToken);
                if (culture == null)
                {
                    throw new QueryException("Данная культура не существует");
                }
                item.Culture = culture;
                var region = await _regionRepository.GetItem(item.Region.Id, cancellationToken);
                if (region == null)
                {
                    throw new QueryException("Данный регион не существует");
                }
                item.Region = region;

                await _repository.AddItem(item, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.Message.Contains(ContextConstants.ProductivityUNIndex))
                        throw new QueryException("Запись данного региона, с данной культурой," +
                            " за данный год уже существует");
                throw;
            }
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

        public override async Task UpdateItem(Guid Id, ProductivityPostDTO record, CancellationToken cancellationToken)
        {
            try
            {
                Shared.Models.Entity.Productivity item = _mapper.Map<Shared.Models.Entity.Productivity>(record);
                var culture = await _cultureRepository.GetItem(item.Culture.Id, cancellationToken);
                if (culture == null)
                {
                    throw new QueryException("Данная культура не существует");
                }
                item.Culture = culture;
                var region = await _regionRepository.GetItem(item.Region.Id, cancellationToken);
                if (region == null)
                {
                    throw new QueryException("Данный регион не существует");
                }
                item.Region = region;
                item.Id = Id;
                await _repository.UpdateItem(item, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.Message.Contains(ContextConstants.ProductivityUNIndex))
                        throw new QueryException("Запись данного региона, с данной культурой," +
                            " за данный год уже существует");
                throw;
            }
        }
    }
}
