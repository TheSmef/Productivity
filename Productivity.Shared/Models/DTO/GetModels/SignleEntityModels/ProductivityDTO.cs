using Productivity.Shared.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.SignleEntityModels
{
    public class ProductivityDTO
    {
        public string Region { get; set; } = string.Empty;
        public Guid RegionId { get; set; } = new Guid();
        public string Culture { get; set; } = string.Empty;
        public Guid CultureId { get; set; } = new Guid();
        public decimal ProductivityValue { get; set; }
        public int Year { get; set; }
    }
}
