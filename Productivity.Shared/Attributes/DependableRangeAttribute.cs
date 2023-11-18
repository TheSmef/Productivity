using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Attributes
{
    public class DependableRangeAttribute : RangeAttribute
    {
        public DependableRangeAttribute(int start, int end)
              : base(typeof(int), DateTime.Today.AddYears(-start).Year.ToString(), DateTime.Today.AddYears(-end).Year.ToString()) { }
    }
}
