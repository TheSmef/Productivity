using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.GetModels.SignleEntityModels.Base
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; } = new Guid();
    }
}
