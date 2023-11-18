using ClosedXML.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.ExportModels
{
    public class ProductivityExcelModel
    {
        [XLColumn(Header = "Регион")]
        public string Region { get; set; } = string.Empty;
        [XLColumn(Header = "Культура")]
        public string Culture { get; set; } = string.Empty;
        [XLColumn(Header = "Стоимость посадки гектара")]
        public decimal CostToPlant { get; set; } = decimal.Zero;
        [XLColumn(Header = "Цена за центнер")]
        public decimal PriceToSell { get; set; } = decimal.Zero;
        [XLColumn(Header = "Урожайность на гектар (центнеров на гектар)")]
        public decimal ProductivityValue { get; set; } = decimal.Zero;
        [XLColumn(Header = "Год")]
        public int Year { get; set; }
    }
}
