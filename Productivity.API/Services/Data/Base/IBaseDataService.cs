using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels.Base;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Services.Data.Base
{
    public interface IBaseDataService<TGetDTO, TPostDTO>
        where TGetDTO : BaseDTO
    {
        public Task AddItem(TPostDTO record, CancellationToken cancellationToken);
        public Task RemoveItem(Guid Id, CancellationToken cancellationToken);
        public Task<CollectionDTO<TGetDTO>> GetItems(QuerySupporter specificatoin, CancellationToken cancellationToken);
        public Task<TGetDTO?> GetItem(Guid Id, CancellationToken cancellationToken);
        public Task UpdateItem(Guid Id, TPostDTO record, CancellationToken cancellationToken);
    }
}
