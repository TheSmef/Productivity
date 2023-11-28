using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.StatsModels.SingleModels
{
    public class CultureStatsModel
    {
        public string Culture { get; set; } = string.Empty;
        public decimal ProductivityValue { get; set; } = decimal.Zero;
    }
}
