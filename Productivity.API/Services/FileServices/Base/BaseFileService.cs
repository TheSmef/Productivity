﻿using AutoMapper;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.Shared.Models.DTO.File.ExportModels;
using Productivity.Shared.Models.DTO.File;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ExportImportHelpers;
using Productivity.API.Services.ExportServices.Base;
using Productivity.Shared.Models.Entity.Base;
using Productivity.API.Services.Data.Base;
using Productivity.API.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Productivity.Shared.Utility.Exceptions;

namespace Productivity.API.Services.FileServices.Base
{
    public abstract class BaseFileService<TEntity, TFileModel> : IFileService<TEntity, TFileModel>
        where TEntity : BaseEntity
        where TFileModel : class
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<TEntity> _repository;
        protected readonly string _worksheet = string.Empty;

        protected BaseFileService(IRepository<TEntity> repository, IMapper mapper,
            string worksheet)
        {
            _repository = repository;
            _mapper = mapper;
            _worksheet = worksheet;
        }

        public async Task<FileModel> ExportItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            var items = _mapper.ProjectTo<TFileModel>(_repository.GetItems(specification, cancellationToken));
            var file = ExcelExporter.GetExcelReport(await items.ToListAsync(cancellationToken), _worksheet);
            return new FileModel() { Data = file, Name = $"{_worksheet}_{DateTime.Today.ToShortDateString()}" };
        }

        public virtual async Task ImportItems(byte[] bytes, CancellationToken cancellationToken)
        {
            var items = ExcelExporter.GetImportModel<TFileModel>(bytes, _worksheet);
            var itemsToAdd = new List<TEntity>();
            foreach(var item in items)
            {
                if (item == null)
                {
                    throw new DataException($"Пустой элемент на строке {items.FindIndex(x => x == null) + 1}");
                }
                ValidationContext validationContext
                        = new ValidationContext(item);
                List<ValidationResult> results = new();
                if (!Validator.TryValidateObject(item, validationContext, results, true))
                {
                    throw new DataException(results.Select(x => x.ErrorMessage).ToList(), $"Ошибка на строке {items.FindIndex(x => x == item) + 1}");
                }
                var itemToAdd = _mapper.Map<TEntity>(item);
                List<string?> validationResults = await _repository.CheckValidate(itemToAdd, cancellationToken);
                if (validationResults != null)
                {
                    if (validationResults.Count > 0)
                    {
                        throw new DataException(validationResults, $"Ошибка на строке {items.FindIndex(x => x == item) + 1}");
                    }
                }
                itemsToAdd.Add(itemToAdd);
            }

            await _repository.AddRange(itemsToAdd, cancellationToken);
        }
    }
}
