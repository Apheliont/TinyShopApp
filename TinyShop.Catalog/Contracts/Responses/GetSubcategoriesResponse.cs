using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record GetSubcategoriesResponse
    {
        public List<CategoryDto> Subcategories { get; set; }
    }
}
