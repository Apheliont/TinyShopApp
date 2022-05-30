using TinyShop.Catalog.DTOs;

namespace TinyShop.Catalog.CategoryFilters
{
    public class TvSetCategoryFilter : CategoryFilter
    {
        [FilterAttribute("Voltage")]
        public List<string> Voltage { get; set; } = null!;
        [FilterAttribute("Subwoofer")]
        public bool HasSubwoofer { get; set; }
        [FilterAttribute("Screen resolution")]
        public List<string> ScreenResolution { get; set; } = null!;
        [FilterAttribute("Screen size", "inch")]
        public RangeDto<double> ScreenSize { get; set; } = null!;
    }
}
