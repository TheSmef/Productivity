using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.File
{
    public class FileModel
    {
        public string Name { get; set; } = string.Empty;
        public byte[] Data { get; set; } = [];
    }
}
