using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Controllers.MessagingControllers.Base;
using Productivity.API.Services.Messaging.Base;
using Productivity.API.Services.Messaging.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels;

namespace Productivity.API.Controllers.MessagingControllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductivityReportController : BaseReportController<ProductivityReportModel>
    {
        public ProductivityReportController(IProductivityReportMessagingService service) : base(service) { }
    }
}
