using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record FilterProductsResponse
    {
        public ProductsInfoDto ProductsInfo { get; set; } = null!;
    }
}
