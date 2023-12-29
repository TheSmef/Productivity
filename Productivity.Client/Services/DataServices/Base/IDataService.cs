using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.Utility;

namespace Productivity.Client.Services.DataServices.Base
{
    public interface IDataService<TDTO>
    {
        public Task<CollectionDTO<TDTO>> Get(QuerySupporter query);
        public Task<TDTO> GetById(Guid id);
    }
}
