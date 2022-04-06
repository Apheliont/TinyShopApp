using MassTransit;
using TinyShop.ProductSearch.Data;
using TinyShop.ProductSearch.Models;

namespace TinyShop.ProductSearch.Consumers
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
