using Productivity.Shared.Attributes;
using Productivity.Shared.Models.DTO.BrokerModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.BrokerModels
{
    public class CultureReportModel : BaseReportModel
    {
        [GuidNotNull(ErrorMessage = "Регион обязателен для ввода!")]
        public Guid RegionId { get; set; } = new Guid();
        [Required(ErrorMessage = "Запрашиваемы год обязателен для ввода!")]
        [DependableRange(-1, -2, ErrorMessage = "Запрашиваемый год должен быть между {1} и {2}")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Желаемый доход обязателен для ввода!")]
        [Range(0.01, 999999999999.99, ErrorMessage = "Желаемый доход должен быть больше 0 и меньше 15 символов до запятой и 2 символов после запятой")]
        public decimal DesiredOutcome { get; set; }
    }
}
