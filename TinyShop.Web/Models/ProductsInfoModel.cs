using System.Collections.Generic;

namespace TinyShop.Web.Models
{
    public class ProductsInfoModel
    {
        public ProductMetadataModel Metadata { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
