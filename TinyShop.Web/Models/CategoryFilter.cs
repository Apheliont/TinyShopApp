namespace TinyShop.Web.Models
{
    public class CategoryFilter
    {
        public int? Index { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public string? Measurement { get; set; }
        public object? Result { get; set; }
    }
}
