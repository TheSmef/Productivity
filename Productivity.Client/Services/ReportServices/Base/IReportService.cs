using Productivity.Shared.Models.DTO.BrokerModels.Base;

namespace Productivity.Client.Services.ReportServices.Base
{
    public interface IReportService<T>
        where T : BaseReportModel
    {
        public Task Send(T model);
    }
}
