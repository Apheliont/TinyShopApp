using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models.ElasticModel
{
    [ElasticsearchType(RelationName = "products", IdProperty = "Id")]
    public record ProductModel
    {
        [Number(Name = "id", Coerce = false)]
        public int Id { get; set; }

        [Text(Name = "productname")]
        public string ProductName { get; set; }

        [Text(Name = "description")]
        public string Description { get; set; }
    }
}
