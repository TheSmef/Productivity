using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.Pipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public override async Task<Result<Unit>> ImportItems(byte[] bytes, CancellationToken cancellationToken)
        {
            var itemsresult = ExcelExporter.GetImportModel<ProductivityFileModel>(bytes, _worksheet);
            if (itemsresult.IsFaulted)
            {
                Exception exception = default!;
                itemsresult.IfFail(ex => exception = ex);
                return new Result<Unit>(exception);
            }
            List<ProductivityFileModel> items = [];
            itemsresult.IfSucc(succ => items = succ);
            var result = ListValidator.Validate(items);
            if (result.IsFaulted)
            {
                return result;
            }
            List<Culture> cultures = _mapper.Map<List<Culture>>(items.DistinctBy(x => x.Culture));
            for (int i = 0; i < cultures.Count; i++)
                cultures[i] = await _cultureRepository.EnsureCreated(_mapper.Map<Culture>(cultures[i]), cancellationToken);
            List<Region> regions = _mapper.Map<List<Region>>(items.DistinctBy(x => x.Region));
            for (int i = 0; i < regions.Count; i++)
                regions[i] = await _regionRepository.EnsureCreated(_mapper.Map<Region>(regions[i]), cancellationToken);
            var itemsToAdd = new List<Shared.Models.Entity.Productivity>();
            for (int i = 0; i < items.Count; i++)
            {
                var itemToAdd = _mapper.Map<Shared.Models.Entity.Productivity>(items[i]);
                itemToAdd.Region = regions.First(x => x.Name.Equals(itemToAdd.Region.Name, StringComparison.CurrentCultureIgnoreCase));
                itemToAdd.Culture = cultures.First(x => x.Name.Equals(itemToAdd.Culture.Name, StringComparison.CurrentCultureIgnoreCase));
                List<string?> validationResults = [.. await _repository.Validate(itemToAdd, cancellationToken),
                    .. _repository.ValidateCollection(itemToAdd, itemsToAdd)];
                if (validationResults != null)
                {
                    if (!validationResults.IsNullOrEmpty())
                    {
                        return new Result<Unit>(new DataException(validationResults, $"Ошибка на строке {i + 1}"));
                    }
                }
                itemsToAdd.Add(itemToAdd);
            }
            await _repository.AddRange(itemsToAdd, cancellationToken);
            return Unit.Default;
        }
    }
}
