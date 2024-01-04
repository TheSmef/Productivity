using AutoMapper;
using Productivity.MailService.Data.Repositories.Interfaces;
using Productivity.MailService.Services.Sender.Base;
using Productivity.MailService.Services.Sender.Interfaces;
using Productivity.Shared.Models.DTO.BrokerModels.SendModels.Base;
using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Productivity.MailService.Services.Base
{
    public class BaseService<T> : IBaseService
        where T : BaseMailModel
    {
        private readonly IMailRepository _repository;
        private readonly ILogger<BaseService<T>> _logger;
        private readonly IMapper _mapper;
        private readonly MailType _type;
        private readonly IBaseSenderService _service;
        public BaseService(IMailRepository repository, ILogger<BaseService<T>> logger,
            IMapper mapper, MailType type, IBaseSenderService service)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _type = type;
            _service = service;
        }

        public async Task Send(string record, CancellationToken cancellationToken)
        {
            try
            {
                var item = _mapper.Map<Mail>(JsonSerializer.Deserialize<T>(record)!);
                item.Type = _type;
                await _service.Send(item);
                await _repository.AddItem(item, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
            }
        }
    }
}
