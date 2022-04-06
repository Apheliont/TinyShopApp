using Nest;

namespace TinyShop.ProductSearch.Models
{
    [ElasticsearchType(RelationName = "products", IdProperty = "Id")]
    public record ElasticProductModel
    {
        [Number(Name = "id", Coerce = false)]
        public int Id { get; set; }

        [Text(Name = "productname")]
        public string ProductName { get; set; }

        [Text(Name = "description")]
        public string Description { get; set; }
    }
}
