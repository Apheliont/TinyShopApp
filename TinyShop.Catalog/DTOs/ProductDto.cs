using System.Dynamic;

namespace TinyShop.Catalog.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public List<ImageDto> Images { get; set; } = new();
        public ExpandoObject? Details { get; set; }
    }
}
