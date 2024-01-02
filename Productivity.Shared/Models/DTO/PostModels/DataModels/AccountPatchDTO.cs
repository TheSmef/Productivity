using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.PostModels.DataModels
{
    public class AccountPatchDTO
    {
        [Required(ErrorMessage = "Электронная почта обязательна для ввода!")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Неправильный формат электронной почты!")]
        [MaxLength(255, ErrorMessage = "Электронная почта не может быть более 255 символов!")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Логин обязателен для ввода!")]
        [MinLength(3, ErrorMessage = "Логин не может быть меньше 3 символов!")]
        [MaxLength(30, ErrorMessage = "Логин не может быть более 30 символов!")]
        [RegularExpression(pattern: "^[a-zA-Z0-9]+$",
            ErrorMessage = "Логин должен содержать в себе только буквы латинского алфавита и цифры!")]
        public string Login { get; set; } = string.Empty;
    }
}
