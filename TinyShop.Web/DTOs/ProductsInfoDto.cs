using System.Collections.Generic;

namespace TinyShop.Web.DTOs
{
    public class ProductsInfoDto
    {
        public ProductMetadataDto Metadata { get; set; } = null!;
        public List<ProductDto>? Products { get; set; }
    }
}
