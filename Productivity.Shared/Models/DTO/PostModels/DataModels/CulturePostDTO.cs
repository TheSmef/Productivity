using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.PostModels.DataModels
{
    public class CulturePostDTO
    {
        [Required(ErrorMessage = "Название культуры обязательно для ввода!")]
        [MinLength(3, ErrorMessage = "Название культуры не может быть меньше 3 символов!")]
        [MaxLength(40, ErrorMessage = "Название культуры не может быть более 40 символов!")]
        [RegularExpression(pattern: "^[а-яА-Я ]+$",
            ErrorMessage = "Название культуры должно содержать в себе только буквы кириллицы!")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Стоимость посадки гектара обязательна для ввода!")]
        [Range(0.01, 999999999999.99, ErrorMessage = "Значение стоимости должно быть больше 0 и меньше 15 символов до запятой и 2 символов после запятой")]
        public decimal CostToPlant { get; set; }
        [Required(ErrorMessage = "Цена за гектар обязательна для ввода!")]
        [Range(0.01, 999999999999.99, ErrorMessage = "Сумма цены должна быть больше 0 и меньше 15 символов до запятой и 2 символов после запятой")]
        public decimal PriceToSell { get; set; }
    }
}
