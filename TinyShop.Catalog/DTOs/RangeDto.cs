namespace TinyShop.Catalog.DTOs
{
    public record RangeDto
    {
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public string Measurement { get; set; }
    }
}
