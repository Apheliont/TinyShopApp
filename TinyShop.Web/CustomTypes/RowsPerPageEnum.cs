using System.ComponentModel;

namespace TinyShop.Web.CustomTypes
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
