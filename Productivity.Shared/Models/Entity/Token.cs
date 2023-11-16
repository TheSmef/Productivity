using Productivity.Shared.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Entity
{
    public class Token : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string TokenStr { get; set; } = string.Empty;
        [Required]
        public Account Account { get; set; } = new Account();
    }
}
