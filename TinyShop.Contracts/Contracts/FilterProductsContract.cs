using System.Dynamic;

namespace TinyShop.Contracts
{
    public record FilterProductsContract
    {
        public ExpandoObject Filter { get; init; }
    }
}
