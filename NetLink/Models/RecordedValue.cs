using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLink.Models
{
    public class RecordedValue
    {
        public RecordedValue(string? value)
        {
            Value = value;
        }

        public string? Value { get; set; }
    }
}
