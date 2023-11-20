using Productivity.API.Services.Data.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Services.Data
{
    public class RegionService : IRegionService
    {
        public Task AddItem(RegionPostDTO record, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<RegionDTO> GetItem(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CollectionDTO<RegionDTO>> GetItems(QuerySupporter specificatoin, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItem(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItem(Guid Id, RegionPostDTO record, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
