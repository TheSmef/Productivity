using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.BrokerModels
{
    public class ProductivityReportModel
    {
        public string Region { get; set; } = string.Empty;
        public Guid RegionId { get; set; } = new Guid();
        public string Culture { get; set; } = string.Empty;
        public Guid CultureId { get; set; } = new Guid();
        public int Year { get; set; }
    }
}
