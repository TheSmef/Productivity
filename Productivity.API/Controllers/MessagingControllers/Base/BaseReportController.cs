using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Messaging.Base;
using Productivity.API.Services.Messaging.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;
using Productivity.Shared.Models.DTO.BrokerModels.Base;
using Productivity.Shared.Utility.Exceptions.Handlers;

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
            var result = await _service.SendRequest(record, cancellationToken);
            return result.Match<ActionResult>(
                succ =>
                {
                    return Ok();
                },
                err =>
                {
                    return BadRequest(ExceptionMapper.Map(err));
                }
                );
        }
    }
}
