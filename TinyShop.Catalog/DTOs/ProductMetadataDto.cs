
namespace TinyShop.Catalog.DTOs
{
    public record ProductMetadataDto
    {
        public int FoundRecords { get; set; }
        public RangeDto<int>? Price { get; set; }
        public List<CategoryFilterDto>? CategoryFilters { get; set; }
    }
}
