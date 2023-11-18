using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.PostModels.AccountModels
{
    public class AuthDTO
    {
        [Required(ErrorMessage = "Логин/Электронная почта - необходимое поле")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "Пароль - необходимое поле")]
        public string Password { get; set; } = string.Empty;
    }
}
