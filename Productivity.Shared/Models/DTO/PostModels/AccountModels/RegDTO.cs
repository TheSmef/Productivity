using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.PostModels.AccountModels
{
    public class RegDTO
    {
        [Required(ErrorMessage = "Электронная почта обязательна для ввода!")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Неправильный формат электронной почты!")]
        [MaxLength(255, ErrorMessage = "Электронная почта не может быть более 255 символов!")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Пароль обязателен для ввода!")]
        [MaxLength(30, ErrorMessage = "Пароль не может быть более 30 символов!")]
        [RegularExpression(pattern: "^(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{8,30}$",
            ErrorMessage = "Пароль должен быть 8-30 символов, содержать в себе как минимум одну букву, как минимум 1 цифру и как минимум 1 символ (!@#$%^&*)")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Подтверждение пароля обязательно для ввода!")]
        [Compare(nameof(Password), ErrorMessage = "Введённые пароли не совпадают!")]
        public string PasswordConfirm { get; set; } = string.Empty;
        [Required(ErrorMessage = "Логин обязателен для ввода!")]
        [MinLength(3, ErrorMessage = "Логин не может быть меньше 3 символов!")]
        [MaxLength(40, ErrorMessage = "Логин не может быть более 40 символов!")]
        [RegularExpression(pattern: "^[a-zA-Z0-9]+$",
            ErrorMessage = "Логин должен содержать в себе только буквы латинского алфавита и цифры!")]
        public string Login { get; set; } = string.Empty;
    }
}
