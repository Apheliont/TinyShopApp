using System.Dynamic;
using TinyShop.Catalog.CustomTypes;

namespace TinyShop.Catalog.DTOs
{
    public record ProductMetadataDto
    {
        public int FoundRecords { get; set; }
        public RangeDto Price { get; set; }
        public ExpandoObject Details { get; set; }
    }
}
