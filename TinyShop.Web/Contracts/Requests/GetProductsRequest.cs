namespace TinyShop.Contracts
{
    public record GetProductsRequest
    {
        public int[] Ids { get; init; } = null!;
    }
}
