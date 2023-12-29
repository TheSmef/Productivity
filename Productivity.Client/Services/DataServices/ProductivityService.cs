using Productivity.Client.Constants;
using Productivity.Client.Services.DataServices.Base;
using Productivity.Client.Services.DataServices.Interfaces;
using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using Productivity.Shared.Models.DTO.PostModels.DataModels;

namespace Productivity.Client.Services.DataServices
{
    public class ProductivityService : BaseDataService<ProductivityDTO>, IProductivityService
    {
        public ProductivityService(IHttpClientFactory factory) : base(factory, ApiPaths.ProductivityPath) { }
    }
}
