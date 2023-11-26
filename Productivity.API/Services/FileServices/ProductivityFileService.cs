using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.ExportServices.Base;
using Productivity.API.Services.ExportServices.Interfaces;
using Productivity.API.Services.FileServices.Base;
using Productivity.Shared.Models.DTO.BrokerModels;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.DTO.File.ExportModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.Exceptions;
using Productivity.Shared.Utility.ExportImportHelpers;
using Productivity.Shared.Utility.Validators;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Productivity.API.Services.ExportServices
{
    public class ProductivityFileService : BaseFileService<Shared.Models.Entity.Productivity, ProductivityFileModel>, IProductivityFileService
    {
        private readonly ICultureRepository _cultureRepository;
        private readonly IRegionRepository _regionRepository;
        public ProductivityFileService(IProductivityRepository repository, IMapper mapper,
            IRegionRepository regionRepository, ICultureRepository cultureRepository) : base(repository, mapper, "Урожайность")
        {
            _cultureRepository = cultureRepository;
            _regionRepository = regionRepository;
        }

        public override async Task ImportItems(byte[] bytes, CancellationToken cancellationToken)
        {
            var items = ExcelExporter.GetImportModel<ProductivityFileModel>(bytes, _worksheet);
            ListValidator.Validate(items);
            List<Culture> cultures = _mapper.Map<List<Culture>>(items.DistinctBy(x => x.Culture));
            for (int i =0; i < cultures.Count; i++)
                cultures[i] = await _cultureRepository.EnsureCreated(_mapper.Map<Culture>(cultures[i]), cancellationToken);
            List<Region> regions = _mapper.Map<List<Region>>(items.DistinctBy(x => x.Region));
            for (int i = 0; i < regions.Count; i++)
                regions[i] = await _regionRepository.EnsureCreated(_mapper.Map<Region>(regions[i]), cancellationToken);
            List<Shared.Models.Entity.Productivity> itemsToAdd = 
                _mapper.Map<List<Shared.Models.Entity.Productivity>>(items);
            foreach (var item in itemsToAdd)
            {
                item.Region = regions.First(x => x.Name.Equals(item.Region.Name, StringComparison.CurrentCultureIgnoreCase));
                item.Culture = cultures.First(x => x.Name.Equals(item.Culture.Name, StringComparison.CurrentCultureIgnoreCase));
                List<string?> validationResults = [.. await _repository.CheckValidate(item, cancellationToken),
                    .. _repository.CheckValidateCollection(item, itemsToAdd)];
                if (validationResults != null)
                {
                    if (validationResults.Count > 0)
                    {
                        throw new DataException(validationResults, $"Ошибка на строке {itemsToAdd.FindIndex(x => x == item) + 1}");
                    }
                }
            }
            await _repository.AddRange(itemsToAdd, cancellationToken);
        }
    }
}
