namespace TinyShop.Catalog.DTOs
{
    public record RangeDto<T>
    {
        public T LowerBound { get; set; } = default!;
        public T UpperBound { get; set; } = default!;
    }
}
