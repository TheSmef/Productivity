using Productivity.Client.Constants;
using Productivity.Client.Services.DataServices.Base;
using Productivity.Client.Services.DataServices.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;

namespace Productivity.Client.Services.DataServices
{
    public class RegionService : BaseDataService<RegionDTO>, IRegionService
    {
        public RegionService(IHttpClientFactory factory) : base(factory, ApiPaths.RegionPath) { }
    }
}
