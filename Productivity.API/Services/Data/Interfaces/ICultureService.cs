using Productivity.API.Services.Data.Base;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;

namespace Productivity.API.Services.Data.Interfaces
{
    public interface ICultureService : IBaseDataService<CultureDTO, CulturePostDTO> { }
}
