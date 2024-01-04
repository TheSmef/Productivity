using AutoMapper;
using Productivity.MailService.Data.Repositories.Interfaces;
using Productivity.MailService.Services.Base;
using Productivity.MailService.Services.Interfaces;
using Productivity.MailService.Services.Sender.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels.SendModels;
using Productivity.Shared.Models.DTO.BrokerModels.SendModels.Base;
using Productivity.Shared.Models.Entity;

namespace Productivity.MailService.Services
{
    public class MailService : BaseService<MailModel>, IMailService
    {
        public MailService(IMailRepository repository, ILogger<MailService> logger,
            IMapper mapper, IMailSenderService service)
            : base(repository, logger, mapper, MailType.Email, service) { }
    }
}
