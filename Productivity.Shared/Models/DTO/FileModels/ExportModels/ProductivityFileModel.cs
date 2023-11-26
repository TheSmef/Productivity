using ClosedXML.Attributes;
using Productivity.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.File.ExportModels
{
    public class ProductivityFileModel
    {
        [XLColumn(Header = "Регион")]
        [Required(ErrorMessage = "Название региона обязательно для ввода!")]
        [MinLength(3, ErrorMessage = "Название региона не может быть меньше 3 символов!")]
        [MaxLength(40, ErrorMessage = "Название региона не может быть более 40 символов!")]
        [RegularExpression(pattern: "^[а-яА-Я-. ]+$",
            ErrorMessage = "Название региона должно содержать в себе только буквы кириллицы!")]
        public string Region { get; set; } = string.Empty;
        [XLColumn(Header = "Культура")]
        [Required(ErrorMessage = "Название культуры обязательно для ввода!")]
        [MinLength(3, ErrorMessage = "Название культуры не может быть меньше 3 символов!")]
        [MaxLength(40, ErrorMessage = "Название культуры не может быть более 40 символов!")]
        [RegularExpression(pattern: "^[а-яА-Я-. ]+$",
            ErrorMessage = "Название культуры должно содержать в себе только буквы кириллицы!")]
        public string Culture { get; set; } = string.Empty;
        [XLColumn(Header = "Стоимость посадки гектара")]
        [Required(ErrorMessage = "Стоимость посадки гектара обязательна для ввода!")]
        [Range(0.01, 999999999999.99, ErrorMessage = "Значение стоимости должно быть больше 0 и меньше 15 символов до запятой и 2 символов после запятой")]
        public decimal CostToPlant { get; set; } = decimal.Zero;
        [XLColumn(Header = "Цена за центнер")]
        [Required(ErrorMessage = "Цена за гектар обязательна для ввода!")]
        [Range(0.01, 999999999999.99, ErrorMessage = "Сумма цены должна быть больше 0 и меньше 15 символов до запятой и 2 символов после запятой")]
        public decimal PriceToSell { get; set; } = decimal.Zero;
        [XLColumn(Header = "Урожайность на гектар (центнеров на гектар)")]
        [Required(ErrorMessage = "Значение урожайности обязательно для ввода!")]
        [Range(0.01, 999999999999.99, ErrorMessage = "Значение урожайности должно быть больше 0 и меньше 15 символов до запятой и 2 символов после запятой")]
        public decimal ProductivityValue { get; set; } = decimal.Zero;
        [XLColumn(Header = "Год")]
        [Required(ErrorMessage = "Год записи обязателен для ввода!")]
        [DependableRange(10, 0, ErrorMessage = "Год записи должен быть между {1} и {2}")]
        public int Year { get; set; }
    }
}
