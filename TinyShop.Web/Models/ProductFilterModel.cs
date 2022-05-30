using System.Collections.Generic;
using TinyShop.Web.CustomTypes;

namespace TinyShop.Web.Models
{
    public class ProductFilterModel
    {
        public int CategoryId { get; set; }
        public RowsPerPageEnum RowsPerPage { get; set; } = RowsPerPageEnum._25;
        public int PageNumber { get; set; } = 1;
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ProductName;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.DESC;
        public RatingModel? Rating { get; set; } = new();
        public RangeModel<int> Price { get; set; } = new();
        public List<CategoryFilter>? CategoryFilters { get; set; }
    }
}
