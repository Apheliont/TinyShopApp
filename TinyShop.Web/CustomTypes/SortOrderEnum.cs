using System.ComponentModel;

namespace TinyShop.Web.CustomTypes
{
    public enum SortOrderEnum
    {
        [Description("Reverse order")]
        DESC = 1,
        [Description("Straight order")]
        ASC = 2
    };
}
