namespace TinyShop.Catalog.DTOs
{
    public class ProductsInfoDto
    {
        public ProductMetadataDto Metadata { get; set; } = new();
        public List<ProductDto>? Products { get; set; }
    }
}
