using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.SignleEntityModels
{
    public class CultureDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal CostToPlant { get; set; }
        public decimal PriceToSell { get; set; }
    }
}
