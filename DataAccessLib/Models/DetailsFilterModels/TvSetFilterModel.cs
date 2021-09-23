using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public class TvSetFilterModel : DetailsFilterModel
    {
        public CheckboxListType YearOfManufacture { get; set; }
        public RangeType ScreenSize { get; set; }

        public RangeType RefreshRate { get; set; }
        public CheckboxListType MatrixType { get; set; }
        public CheckboxListType ScreenResolution { get; set; }
        public RangeType Height { get; set; }
        public RangeType Width { get; set; }
        public RangeType Depth { get; set; }
        public RangeType Weight { get; set; }



        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Wifi { get; set; }
    }
}
