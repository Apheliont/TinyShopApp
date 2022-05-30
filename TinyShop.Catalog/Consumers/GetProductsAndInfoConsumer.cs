using MassTransit;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Repositories;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Consumers
{
    public class GetProductsAndInfoConsumer : IConsumer<GetProductsAndInfoRequest>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsAndInfoConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<GetProductsAndInfoRequest> context)
        {
            ProductFilterDto productFilter = context.Message.Filter;
            if (productFilter is null)
            {
                throw new InvalidOperationException("Request data not found");
            }

            var foundProducts = await _productRepository.GetProductsAndInfo(productFilter);

            await context.RespondAsync(new GetProductsAndInfoResponse { ProductsInfo = foundProducts });
        }
    }
}
