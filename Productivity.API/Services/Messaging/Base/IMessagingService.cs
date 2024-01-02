using LanguageExt;
using LanguageExt.Common;
using Productivity.Shared.Models.DTO.BrokerModels.Base;

namespace Productivity.API.Services.Messaging.Base
{
    public interface IMessagingService<T>
        where T : BaseReportModel
    {
        public Task<Result<Unit>> SendRequest(T request, CancellationToken cancellationToken);
    }
}
