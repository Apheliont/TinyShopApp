using System.Collections.Generic;
using TinyShop.Web.DTOs;

namespace TinyShop.Contracts
{
    public record GetProductsResponse
    {
        public List<ProductDto> Products { get; set; }
    }
}
