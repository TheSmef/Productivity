using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.StatsModels.CollectionModels
{
    public class StatsModel<T>
    {
        public string Param { get; set; } = string.Empty;
        public ICollection<T> Stats { get; set; } = new List<T>();
    }
}
