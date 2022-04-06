using System.Collections.Generic;
using TinyShop.Web.CustomTypes;

namespace TinyShop.Web.DTOs
{
    public class ProductFilterDto
    {
        public int RowsPerPage { get; set; } = 25;
        public int PageNumber { get; set; } = 1;
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ProductName;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.DESC;
        public int Rating { get; set; }
        public RangeDtoIn Price { get; set; } = new();
        public Dictionary<string, KeyValuePair<QueryOperatorEnum, object>> Filters { get; set; }
    }
}
