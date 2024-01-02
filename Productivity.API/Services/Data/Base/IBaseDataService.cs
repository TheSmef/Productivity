using LanguageExt;
using LanguageExt.Common;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels.Base;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Services.Data.Base
{
    public interface IBaseDataService<TDTO, TPostDTO>
        where TDTO : BaseDTO
    {
        public Task<Result<TDTO>> AddItem(TPostDTO record, CancellationToken cancellationToken);
        public Task<Result<Unit>> RemoveItem(Guid Id, CancellationToken cancellationToken);
        public Task<Result<CollectionDTO<TDTO>>> GetItems(QuerySupporter specificatoin, CancellationToken cancellationToken);
        public Task<TDTO?> GetItem(Guid Id, CancellationToken cancellationToken);
        public Task<Result<TDTO>> UpdateItem(Guid Id, TPostDTO record, CancellationToken cancellationToken);
    }
}
