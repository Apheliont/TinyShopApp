namespace TinyShop.Catalog.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public ImageDto? Image { get; set; }
        public bool IsParent { get; set; }
    }
}
