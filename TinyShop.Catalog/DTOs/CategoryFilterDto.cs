using Newtonsoft.Json;

namespace TinyShop.Catalog.DTOs
{
    public class CategoryFilterDto
    {
        public int? Index { get; set; } = 0;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public string? Measurement { get; set; }
        public object? Result { get; set; }
    }
}
