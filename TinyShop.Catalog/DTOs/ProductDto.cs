namespace TinyShop.Catalog.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<ImageDto> Images { get; set; } = new();
        public Dictionary<string, dynamic> Details { get; set; } = new();
    }
}
