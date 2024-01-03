using AutoMapper;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Stats.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.ModelHelpers;

namespace Productivity.API.Services.Stats
{
    public class RegionStatsService : IRegionStatsService
    {
        private readonly IProductivityRepository _repository;
        private readonly IMapper _mapper;

        public RegionStatsService(IProductivityRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CollectionDTO<CultureStatsModel>>> GetStats(StatsDistinctQuery query, CancellationToken cancellationToken)
        {
            var items = _mapper.ProjectTo<CultureStatsModel>(_repository
                .GetItems(cancellationToken, x => x.Year == query.Year && x.Region.Id == query.Id).AsNoTracking());
            var result = await ResponceModelBuilder.Build(query.Top,
                query.Skip, items, cancellationToken);
            return result;
        }
    }
}
