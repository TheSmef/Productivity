using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Stats.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Models.Utility.Base;
using Productivity.Shared.Utility.ModelHelpers;

namespace Productivity.API.Services.Stats
{
    public class ProductivityStatsService : IProductivityStatsService
    {
        private readonly IRegionRepository _repository;

        public ProductivityStatsService(IRegionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<CollectionDTO<ProductivityStatsModel>>> GetStats(StatsQuery query, CancellationToken cancellationToken)
        {
            var items = _repository.GetItems(cancellationToken).AsNoTracking()
                .Select(x => new ProductivityStatsModel()
                {
                    Region = x.Name,
                    Productivity = x.Productivities.Where(x => x.Culture.Id == query.Id)
                        .Select(x => x.ProductivityValue).DefaultIfEmpty().Average(),
                })
            .Where(x => x.Productivity != 0);
            var result = await ResponceModelBuilder.Build(query.Top,
                query.Skip, items, cancellationToken);
            return result;
        }

    }
}
