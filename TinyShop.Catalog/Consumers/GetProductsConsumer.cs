using MassTransit;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Repositories;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Consumers
{
    public class GetProductsConsumer : IConsumer<GetProductsRequest>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<GetProductsRequest> context)
        {
            List<ProductDto> dtos = await _productRepository.GetProducts(context.Message.Ids);
            await context.RespondAsync(new GetProductsResponse { Products = dtos });
        }
    }
}
