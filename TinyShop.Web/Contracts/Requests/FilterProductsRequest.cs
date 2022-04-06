using System.Dynamic;

namespace TinyShop.Contracts
{
    public record FilterProductsRequest
    {
        public ExpandoObject Filter { get; init; }
    }
}
