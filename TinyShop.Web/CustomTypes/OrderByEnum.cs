using System.ComponentModel;

namespace TinyShop.Web.CustomTypes
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
