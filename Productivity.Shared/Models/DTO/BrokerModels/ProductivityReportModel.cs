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
    public class ProductivityReportModel : BaseReportModel
    {
        [GuidNotNull(ErrorMessage = "Регион обязателен для ввода!")]
        public Guid RegionId { get; set; } = new Guid();
        [GuidNotNull(ErrorMessage = "Культура обязательна для ввода!")]
        public Guid CultureId { get; set; } = new Guid();
        [Required(ErrorMessage = "Запрашиваемый год обязателен для ввода!")]
        [DependableRange(-1, -2, ErrorMessage = "Год записи должен быть между {1} и {2}")]
        public int Year { get; set; }
    }
}
