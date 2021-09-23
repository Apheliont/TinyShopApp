using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Enums
{
    public enum OrderByEnum
    {
        [Description("By name")]
        ProductName = 1,
        [Description("By price")]
        Price = 2,
        [Description("By rating")]
        Rating = 3
    };
}
