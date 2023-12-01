using Productivity.Shared.Models.DTO.BrokerModels.Base;

namespace Productivity.API.Services.Messaging.Base
{
    public interface IMessagingService<T>
        where T : BaseReportModel
    {
        public Task SendRequest(T request, CancellationToken cancellationToken);
    }
}
