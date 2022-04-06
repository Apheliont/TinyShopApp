using System.Dynamic;

namespace TinyShop.Web.Models
{
    public record ProductMetadataModel
    {
        public int FoundRecords { get; set; }
        public RangeModel Price { get; set; }
        public ExpandoObject Details { get; set; }
    }
}
