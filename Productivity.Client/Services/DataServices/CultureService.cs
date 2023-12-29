using Productivity.Client.Constants;
using Productivity.Client.Services.DataServices.Base;
using Productivity.Client.Services.DataServices.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;

namespace Productivity.Client.Services.DataServices
{
    public class CultureService : BaseDataService<CultureDTO>, ICultureService
    {
        public CultureService(IHttpClientFactory factory) : base(factory, ApiPaths.CulturePath) { }
    }
}
