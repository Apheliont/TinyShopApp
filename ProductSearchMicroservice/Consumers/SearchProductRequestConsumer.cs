using MassTransit;
using DataAccessLib.Models;
using ProductSearchMicroservice.Data;

namespace ProductSearchMicroservice.Consumers
{
    internal class SearchProductRequestConsumer : IConsumer<ProductSearchRequestModel>
    {
        private readonly IProductService _productService;

        public SearchProductRequestConsumer(IProductService productService)
        {
            _productService = productService;
        }
        public async Task Consume(ConsumeContext<ProductSearchRequestModel> context)
        {
            ProductSearchRequestModel request = context.Message;
            List<int> foundProductIds = await _productService.SearchProducts(request);

            await context.RespondAsync<ProductSearchResponseModel>(new { ProductIds = foundProductIds });
        }
    }
}
