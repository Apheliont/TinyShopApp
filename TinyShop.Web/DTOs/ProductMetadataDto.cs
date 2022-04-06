using System.Dynamic;

namespace TinyShop.Web.DTOs
{
    public record ProductMetadataDto
    {
        public int FoundRecords { get; set; }
        public RangeDtoIn Price { get; set; }
        public ExpandoObject Details { get; set; }
    }
}
