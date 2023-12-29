using Productivity.Client.Constants;
using Productivity.Client.Services.ReportServices.Base;
using Productivity.Client.Services.ReportServices.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;

namespace Productivity.Client.Services.ReportServices
{
    public class CultureReportService : BaseReportService<CultureReportModel>, ICultureReportService
    {
        public CultureReportService(IHttpClientFactory factory) : base(factory, ApiPaths.CultureReportPath) { }
    }
}
