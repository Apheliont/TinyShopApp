namespace TinyShop.Contracts
{
    public record GetBreadcrumbsContract
    {
        public int Id { get; init; }
        public bool IsProduct { get; init; }
    }
}
