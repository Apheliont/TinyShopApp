using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record RangeItem
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public string Measurement { get; set; }
    }
}
