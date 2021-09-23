using Newtonsoft.Json;

namespace DataAccessLib.Models
{
    public record RangeType
    {
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        public string Measurement { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? From { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? To {  get; set; }

    }
}
