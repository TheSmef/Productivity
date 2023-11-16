using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.StatsModel.SingleModel
{
    public class ProductivityStatsModel
    {
        public decimal Productivity { get; set; } = decimal.Zero;
        public int Year { get; set; }
    }
}
