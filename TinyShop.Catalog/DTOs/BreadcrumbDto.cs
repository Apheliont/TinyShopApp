namespace TinyShop.Catalog.DTOs
{
    public class BreadcrumbDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public bool IsProduct { get; set; } = false;
    }
}
