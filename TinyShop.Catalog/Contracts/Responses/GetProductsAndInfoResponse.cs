using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record GetProductsAndInfoResponse
    {
        public ProductsInfoDto ProductsInfo { get; set; } = null!;
    }
}
