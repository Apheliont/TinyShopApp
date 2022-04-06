using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record GetRootCategoriesResponse
    {
        public List<CategoryDto> Categories { get; set; }
    }
}
