using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public class TvSetFilterModel : DetailsFilterModel
    {
        [Description("Year of manufacture")]
        public CheckboxListType YearOfManufacture { get; set; }

        [Description("Screen size")]
        public RangeType ScreenSize { get; set; }
        [Description("Refresh rate")]
        public RangeType RefreshRate { get; set; }
        [Description("Matrix type")]
        public CheckboxListType MatrixType { get; set; }
        [Description("Screen resolution")]
        public CheckboxListType ScreenResolution { get; set; }
        [Description("Height")]
        public RangeType Height { get; set; }
        [Description("Width")]
        public RangeType Width { get; set; }
        [Description("Depth")]
        public RangeType Depth { get; set; }
        [Description("Weight")]
        public RangeType Weight { get; set; }


        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [Description("WiFi")]
        public CheckboxType Wifi { get; } = new();
    }
}
