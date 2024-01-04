using Productivity.Shared.Models.DTO.BrokerModels.SendModels.Base;
using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.BrokerModels.SendModels
{
    public class MailModel : BaseMailModel
    {
        [Required]
        [StringLength(30)]
        public string Subject { get; set; } = string.Empty;
    }
}
