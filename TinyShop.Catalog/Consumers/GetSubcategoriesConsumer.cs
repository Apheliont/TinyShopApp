using MassTransit;
using TinyShop.Catalog.Repositories;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Consumers
{
    public class GetSubcategoriesConsumer : IConsumer<GetSubcategoriesRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetSubcategoriesConsumer(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task Consume(ConsumeContext<GetSubcategoriesRequest> context)
        {
            var dtos = await _categoryRepository.GetSubcategories(context.Message.CategoryId);
            await context.RespondAsync(new GetSubcategoriesResponse { Subcategories = dtos });
        }
    }
}
