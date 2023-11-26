using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.PostModels.DataModels
{
    public class RegionPostDTO
    {
        [Required(ErrorMessage = "Название региона обязательно для ввода!")]
        [MinLength(3, ErrorMessage = "Название региона не может быть меньше 3 символов!")]
        [MaxLength(40, ErrorMessage = "Название региона не может быть более 40 символов!")]
        [RegularExpression(pattern: "^[а-яА-Я-. ]+$",
            ErrorMessage = "Название региона должно содержать в себе только буквы кириллицы!")]
        public string Name { get; set; } = string.Empty;
    }
}
