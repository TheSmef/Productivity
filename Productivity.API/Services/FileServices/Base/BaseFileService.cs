using AutoMapper;
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
using Productivity.Shared.Utility.Validators;
using Productivity.Shared.Utility.ModelHelpers;
using System.Linq.Dynamic.Core.Exceptions;
using Microsoft.IdentityModel.Tokens;
using LanguageExt.Common;
using LanguageExt;

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

        public async Task<Result<FileModel>> ExportItems(QuerySupporter specification, CancellationToken cancellationToken)
        {
            var itemsresult = DataSpecificationQueryBuilder.GetQuery(specification,
                _mapper.ProjectTo<TFileModel>(_repository.GetItems(cancellationToken)));
            if (itemsresult.IsFaulted)
            {
                Exception exception = default!;
                itemsresult.IfFail(ex => exception = ex);
                return new Result<FileModel>(exception);
            }
            var items = Enumerable.Empty<TFileModel>().AsQueryable();
            itemsresult.IfSucc(succ => items = succ);
            var file = ExcelExporter.GetExcelReport(await items.ToListAsync(cancellationToken), _worksheet);
            return new FileModel() { Data = file, Name = $"{_worksheet}_{DateTime.Today.ToShortDateString()}" };
        }

        public virtual async Task<Result<Unit>> ImportItems(Stream stream, CancellationToken cancellationToken)
        {
            var itemsresult = ExcelExporter.GetImportModel<TFileModel>(stream, _worksheet);
            if (itemsresult.IsFaulted)
            {
                Exception exception = default!;
                itemsresult.IfFail(ex => exception = ex);
                return new Result<Unit>(exception);
            }
            List<TFileModel> items = [];
            itemsresult.IfSucc(succ => items = succ);
            var itemsToAdd = new List<TEntity>();
            var result = ListValidator.Validate(items);
            if (result.IsFaulted)
            {
                return result;
            }
            for (int i = 0; i < items.Count; i++)
            {
                var itemToAdd = _mapper.Map<TEntity>(items[i]);
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
