using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public class TvSetMetadataModel : DetailsMetadataModel
    {
        public List<int> YearOfManufacture { get; set; }
        public RangeItem ScreenSize { get; set; }

        public RangeItem RefreshRate { get; set; }
        public List<String> MatrixType { get; set; }
        public List<String> ScreenResolution { get; set; }
        public RangeItem Height { get; set; }
        public RangeItem Width { get; set; }
        public RangeItem Depth { get; set; }
        public RangeItem Weight { get; set; }



        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Wifi { get; set; }
    }
}
