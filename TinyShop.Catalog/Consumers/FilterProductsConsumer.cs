using MassTransit;
using System.Dynamic;
using TinyShop.Catalog.Repositories;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Consumers
{
    public class FilterProductsConsumer : IConsumer<FilterProductsRequest>
    {
        private readonly IProductRepository _productRepository;

        public FilterProductsConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<FilterProductsRequest> context)
        {
            ExpandoObject filter = context.Message.Filter;
            if (filter is null)
            {
                throw new InvalidOperationException("Request data not found");
            }

            var foundProducts = await _productRepository.FilterProducts(filter);

            await context.RespondAsync(new FilterProductsResponse { ProductsInfo = foundProducts });
        }
    }
}
