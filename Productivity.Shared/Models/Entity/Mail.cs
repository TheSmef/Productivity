using Productivity.Shared.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Entity
{
    public class Mail : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string To { get; set; } = string.Empty;
        [StringLength(30)]
        public string? Subject { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string Body { get; set; } = string.Empty;
        [Required]
        public DateTime DateTime { get; private set; } = DateTime.UtcNow;
        [Required]
        public MailType Type { get; set; } = MailType.Email;
    }


    public enum MailType
    {
        Email = 1,
        Telegram = 2
    }
}
