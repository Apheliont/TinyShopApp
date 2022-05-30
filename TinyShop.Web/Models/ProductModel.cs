using System.Collections.Generic;

namespace TinyShop.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public RatingModel Rating { get; set; } = new RatingModel();
        public List<ImageModel> Images { get; set; } = new();
        public Dictionary<string, dynamic>? Details { get; set; }
    }
}
