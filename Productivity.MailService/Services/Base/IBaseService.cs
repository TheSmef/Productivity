using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.MailService.Services.Base
{
    public interface IBaseService
    {
        public Task Send([StringSyntax(StringSyntaxAttribute.Json)] string record, CancellationToken cancellationToken);
    }
}
