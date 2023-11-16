using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.SignleEntityModels
{
    public class AccountDTO : BaseDTO
    {
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
