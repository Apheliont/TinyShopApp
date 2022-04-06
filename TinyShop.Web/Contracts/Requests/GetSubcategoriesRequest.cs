namespace TinyShop.Contracts
{
    public record GetSubcategoriesRequest
    {
        public int CategoryId { get; init; }
    }
}
