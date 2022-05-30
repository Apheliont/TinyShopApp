using TinyShop.Catalog.CustomTypes;
using System.Dynamic;

namespace TinyShop.Catalog.DTOs
{
    public class ProductFilterDto
    {
        public int CategoryId { get; set; }
        public int RowsPerPage { get; set; } = 25;
        public int PageNumber { get; set; } = 1;
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ProductName;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.DESC;
        public RangeDto<int>? Price { get; set; }
        public List<CategoryFilterDto>? CategoryFilters { get; set; }
    }
}
