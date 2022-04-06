using MassTransit;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Repositories;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Consumers
{
    public class GetProductConsumer : IConsumer<GetProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public GetProductConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<GetProductRequest> context)
        {
            ProductDto productDto = await _productRepository.GetProduct(context.Message.Id);
            await context.RespondAsync(new GetProductResponse { Product = productDto });
        }
    }
}
