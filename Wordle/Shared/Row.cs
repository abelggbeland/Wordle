using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Shared
{
    public class Row
    {
        public string RowValue { get; set; } = string.Empty;
        public string Animation { get; set; } = string.Empty;
        public string RowSubmit { get; set; } = string.Empty;
        public bool Sloved { get; set; } = false;
        public bool Loaded { get; set; } = false;
    }
}
