using System.Collections.Generic;

namespace DataAccessLib.Models
{
    public class ProductsWithMetadataModel
    {
        public ProductMetadataModel Metadata { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
