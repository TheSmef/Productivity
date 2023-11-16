using Productivity.Shared.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Entity
{
    public class Productivity : BaseEntity
    {
        [Required]
        public Region Region { get; set; } = new Region();
        [Required]
        public Culture Culture { get; set; } = new Culture();
        [Required]
        public decimal ProductivityValue { get; set; }
        [Required]
        public int Year { get; set; }
    }
}
