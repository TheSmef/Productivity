using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Repositories.Base;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Stats.Base;
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
        private readonly IProductivityRepository _productivityRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public ProductivityStatsService(IRegionRepository regionRepository,
            IProductivityRepository productivityRepository,
            IMapper mapper)
        {
            _regionRepository = regionRepository;
            _productivityRepository = productivityRepository;
            _mapper = mapper;
        }

        public async Task<CollectionDTO<RegionStatsModel>> GetCultureStats(StatsDistinctQuery query, CancellationToken cancellationToken)
        {
            var items = _mapper.ProjectTo<RegionStatsModel>(_productivityRepository
                .GetItems(cancellationToken, x => x.Year == query.Year && x.Culture.Id == query.Id).AsNoTracking());
            CollectionDTO<RegionStatsModel> responce = await ResponceModelBuilder.Build(query.Top,
                query.Skip, items, cancellationToken);
            return responce;
        }

        public async Task<CollectionDTO<ProductivityStatsModel>> GetProductivityStats(StatsQuery query, CancellationToken cancellationToken)
        {
            var items = _regionRepository.GetItems(cancellationToken).AsNoTracking()
                .Select(x => new ProductivityStatsModel()
                {
                    Region = x.Name,
                    Productivity = x.Productivities.Where(x => x.Culture.Id == query.Id)
                        .Select(x => x.ProductivityValue).DefaultIfEmpty().Average(),
                })
            .Where(x => x.Productivity != 0);
            CollectionDTO<ProductivityStatsModel> responce = await ResponceModelBuilder.Build(query.Top,
                query.Skip, items, cancellationToken);
            return responce;
        }


        public async Task<CollectionDTO<CultureStatsModel>> GetRegionStats(StatsDistinctQuery query, CancellationToken cancellationToken)
        {
            var items = _mapper.ProjectTo<CultureStatsModel>(_productivityRepository
                .GetItems(cancellationToken, x => x.Year == query.Year && x.Region.Id == query.Id).AsNoTracking());
            CollectionDTO<CultureStatsModel> responce = await ResponceModelBuilder.Build(query.Top,
                query.Skip, items, cancellationToken);
            return responce;
        }
    }
}
