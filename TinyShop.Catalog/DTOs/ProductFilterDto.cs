using TinyShop.Catalog.CustomTypes;

namespace TinyShop.Catalog.DTOs
{
    public class ProductFilterDto
    {
        public int RowsPerPage { get; set; } = 25;
        public int PageNumber { get; set; } = 1;
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ProductName;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.DESC;
        public Dictionary<string, KeyValuePair<QueryOperatorEnums, object>> Filters { get; set; }
    }
}
