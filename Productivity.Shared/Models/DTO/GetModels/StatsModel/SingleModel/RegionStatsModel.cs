using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.StatsModel.SingleModel
{
    public class RegionStatsModel
    {
        public string Region { get; set; } = string.Empty;
        public decimal Productivity { get; set; } = decimal.Zero;
    }
}
