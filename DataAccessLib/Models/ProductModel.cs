using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Rating { get; set; }

        // Если в jsone нет поля Images и не указать декаратор то получим рантайм ошибку
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ImageModel> Images { get; set; }
    }
}
