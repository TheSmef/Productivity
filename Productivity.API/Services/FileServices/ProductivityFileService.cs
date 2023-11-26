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
            List<Shared.Models.Entity.Productivity> itemsToAdd = new();
            foreach (var item in items)
            {
                if (item == null)
                {
                    throw new DataException($"Пустой элемент на строке {items.FindIndex(x => x == null)}");
                }
                ValidationContext validationContext
                        = new ValidationContext(item);
                List<ValidationResult> results = new();
                if (!Validator.TryValidateObject(item, validationContext, results, true))
                {
                    throw new DataException(results.Select(x => x.ErrorMessage).ToList(), $"Ошибка на строке {items.FindIndex(x => x == item) + 1}");
                }
                Culture culture = await _cultureRepository.EnsureCreated(_mapper.Map<Culture>(item), cancellationToken);
                Region region = await _regionRepository.EnsureCreated(_mapper.Map<Region>(item), cancellationToken);
                Shared.Models.Entity.Productivity productivity =
                    _mapper.Map<Shared.Models.Entity.Productivity>(item);
                productivity.Region = region;
                productivity.Culture = culture;
                List<string?> validationResults = await _repository.CheckValidate(productivity, cancellationToken);
                if (validationResults != null)
                {
                    if (validationResults.Count > 0)
                    {
                        throw new DataException(validationResults, $"Ошибка на строке {items.FindIndex(x => x == item) + 1}");
                    }
                }
                itemsToAdd.Add(productivity);
            }
            await _repository.AddRange(itemsToAdd, cancellationToken);
        }
    }
}
