using MassTransit;
using TinyShop.Catalog.DTOs;
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
            ProductFilterDto productFilter = context.Message.Filter;
            if (productFilter is null)
            {
                throw new InvalidOperationException("Request data not found");
            }

            var foundProducts = await _productRepository.FilterProducts(productFilter);

            await context.RespondAsync(new FilterProductsResponse { ProductsInfo = foundProducts });
        }
    }
}
