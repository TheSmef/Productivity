using Productivity.Shared.Models.DTO.GetModels.SignleEntityModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.CollectionModels
{
    public class CollectionDTO<T>
    {
        public int CurrentPageIndex { get; set; }
        public int TotalPages { get; set; }
        public int ElementsCount { get; set; }
        public ICollection<T> Collection { get; set; } = new List<T>();
    }
}
