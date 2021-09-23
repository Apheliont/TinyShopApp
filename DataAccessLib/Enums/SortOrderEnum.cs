using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Enums
{
    public enum SortOrderEnum
    {
        [Description("Reverse order")]
        DESC = 1,
        [Description("Straight order")]
        ASC = 2
    };
}
