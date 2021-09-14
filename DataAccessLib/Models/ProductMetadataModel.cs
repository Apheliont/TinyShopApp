using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record ProductMetadataModel
    {
        public int FoundRecords { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        // Если в jsone нет поля DetailsMetadata и не указать декаратор то получим рантайм ошибку
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ExpandoObject Details { get; set; }
    }
}
