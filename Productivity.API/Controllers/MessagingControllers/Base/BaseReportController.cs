using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Messaging.Base;
using Productivity.API.Services.Messaging.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;
using Productivity.Shared.Models.DTO.BrokerModels.Base;

namespace Productivity.API.Controllers.MessagingControllers.Base
{
    public abstract class BaseReportController<T> : ControllerBase
        where T : BaseReportModel
    {
        private readonly IMessagingService<T> _service;
        public BaseReportController(IMessagingService<T> service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult> GetCultureStats(T record, CancellationToken cancellationToken)
        {
            await _service.SendRequest(record, cancellationToken);
            return Ok();
        }
    }
}
