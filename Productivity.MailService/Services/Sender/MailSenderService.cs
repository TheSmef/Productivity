using Productivity.MailService.Services.Sender.Interfaces;
using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Services.Sender
{
    public class MailSenderService : IMailSenderService
    {
        public Task Send(Mail record)
        {
            return Task.CompletedTask;
        }
    }
}
