using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record GetProductsAndInfoRequest
    {
        public ProductFilterDto Filter { get; set; } = null!;
    }
}
