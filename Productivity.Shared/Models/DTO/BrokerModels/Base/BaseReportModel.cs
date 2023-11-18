using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.BrokerModels.Base
{
    public abstract class BaseReportModel
    {
        [Required(ErrorMessage = "Электронная почта обязательна для ввода!")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Неправильный формат электронной почты!")]
        [MaxLength(255, ErrorMessage = "Электронная почта не может быть более 255 символов!")]
        public string Email { get; set; } = string.Empty;
    }
}
