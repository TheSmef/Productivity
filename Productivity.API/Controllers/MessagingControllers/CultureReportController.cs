using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.MessagingControllers.Base;
using Productivity.API.Services.Messaging.Base;
using Productivity.API.Services.Messaging.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;
using Productivity.Shared.Models.DTO.BrokerModels.Base;
using Productivity.Shared.Models.DTO.GetModels.CollectionModels;
using Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels;
using Productivity.Shared.Models.Utility;

namespace Productivity.API.Controllers.MessagingControllers
{
    [Route("[controller]")]
    [ApiController]
    public class CultureReportController : BaseReportController<CultureReportModel>
    {
        public CultureReportController(ICultureReportMessagingService service) : base(service) { }
    }
}
