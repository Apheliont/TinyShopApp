using TinyShop.Web.DTOs;

namespace TinyShop.Contracts
{
    public record GetProductResponse
    {
        public ProductDto Product { get; set; }
    }
}
