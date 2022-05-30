using System.Collections.Generic;

namespace TinyShop.Web.Models
{
    public record ProductMetadataModel
    {
        public int FoundRecords { get; set; }
        public RangeModel<int>? Price { get; set; }
        public List<CategoryFilter>? CategoryFilters { get; set; }
    }
}
