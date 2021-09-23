using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Enums
{
    public enum RowsPerPageEnum
    {
        [Description("10")]
        _10 = 10,
        [Description("25")]
        _25 = 25,
        [Description("50")]
        _50 = 50,
        [Description("80")]
        _80 = 80
    }
}
