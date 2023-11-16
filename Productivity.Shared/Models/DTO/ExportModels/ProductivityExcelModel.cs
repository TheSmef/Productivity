using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.ExportModels
{
    public class ProductivityExcelModel
    {
        public string Region { get; set; } = string.Empty;
        public string Culture { get; set; } = string.Empty;
        public decimal CostToPlant { get; set; } = decimal.Zero;
        public decimal PriceToSell { get; set; } = decimal.Zero;
        public decimal ProductivityValue { get; set; } = decimal.Zero;
        public int Year { get; set; }
    }
}
