using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Productivity.Shared.Utility.Constants
{
    public class JsonSerializerOptionsPreset
    {
        public static JsonSerializerOptions Options { get; private set; } = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }; 
    }
}
