using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record FilterProductsRequest
    {
        public ProductFilterDto Filter { get; set; } = null!;
    }
}
