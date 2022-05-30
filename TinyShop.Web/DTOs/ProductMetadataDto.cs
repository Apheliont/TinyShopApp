using System.Collections.Generic;
using System.Dynamic;

namespace TinyShop.Web.DTOs
{
    public record ProductMetadataDto
    {
        public int FoundRecords { get; set; }
        public RangeDto<int>? Price { get; set; }
        public List<CategoryFilterDto>? CategoryFilters { get; set; }
    }
}
