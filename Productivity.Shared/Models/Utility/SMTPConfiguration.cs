using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.Utility
{
    public class SMTPConfiguration
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Server {  get; set; } = string.Empty;
        public int Port { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
