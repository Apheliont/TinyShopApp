namespace TinyShop.Web.Models
{
    public record RangeModel
    {
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public string Measurement { get; set; }
        public double? From { get; set; }
        public double? To { get; set; }

        public void Reset()
        {
            From = null;
            To = null;
        }
    }
}
