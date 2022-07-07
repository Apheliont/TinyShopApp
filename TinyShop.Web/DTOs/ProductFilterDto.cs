using System.Collections.Generic;
using TinyShop.Web.CustomTypes;

namespace TinyShop.Web.DTOs
{
    public class ProductFilterDto
    {
        public int CategoryId { get; set; }
        public int RowsPerPage { get; set; } = 25;
        public int PageNumber { get; set; } = 1;
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ProductName;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.DESC;
        public UserSettingsDto? UserSettings { get; set; }
        public RangeDto<int>? Price { get; set; }
        public List<CategoryFilterDto>? CategoryFilters { get; set; }
    }
}
