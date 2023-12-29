using Productivity.Client.Constants;
using Productivity.Client.Services.ReportServices.Base;
using Productivity.Client.Services.ReportServices.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;

namespace Productivity.Client.Services.ReportServices
{
    public class ProductivityReportService : BaseReportService<ProductivityReportModel>, IProductivityReportService
    {
        public ProductivityReportService(IHttpClientFactory factory) : base(factory, ApiPaths.ProductivityReportPath) { }
    }
}
