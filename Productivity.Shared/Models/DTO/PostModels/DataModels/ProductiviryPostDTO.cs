using Productivity.Shared.Attributes;
using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.PostModels.DataModels
{
    public class ProductiviryPostDTO
    {
        [GuidNotNull(ErrorMessage = "Регион обязателен для ввода!")]
        public Guid Region { get; set; } = new Guid();
        [GuidNotNull(ErrorMessage = "Культура обязательна для ввода!")]
        public Culture Culture { get; set; } = new Culture();
        [Required(ErrorMessage = "Значение урожайности обязательно для ввода!")]
        [Range(0.01, 999999999999.99, ErrorMessage = "Значение урожайности должно быть больше 0 и меньше 15 символов до запятой и 2 символов после запятой")]
        public decimal ProductivityValue { get; set; }
        [Required(ErrorMessage = "Год записи обязателен для ввода!")]
        [DependableRange(10, 0, ErrorMessage = "Год записи должен быть между {1} и {2}")]
        public int Year { get; set; }
    }
}
