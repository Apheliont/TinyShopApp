using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record GetProductsResponse
    {
        public List<ProductDto> Products { get; set; }
    }
}
