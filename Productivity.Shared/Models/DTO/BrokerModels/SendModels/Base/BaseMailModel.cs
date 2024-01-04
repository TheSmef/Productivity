using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.BrokerModels.SendModels.Base
{
    public class BaseMailModel
    {
        [Required]
        [StringLength(30)]
        public string To { get; set; } = string.Empty;
        [Required]
        [StringLength(int.MaxValue)]
        public string Body { get; set; } = string.Empty;
    }
}
