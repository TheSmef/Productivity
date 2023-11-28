using Productivity.Shared.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Entity
{
    public class Region : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Productivity> Productivities { get; set; } = new List<Productivity>();
    }
}
