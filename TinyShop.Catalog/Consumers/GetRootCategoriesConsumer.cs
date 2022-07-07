using MassTransit;
using TinyShop.Catalog.Repositories;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Consumers
{
    public class GetRootCategoriesConsumer : IConsumer<GetRootCategoriesRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetRootCategoriesConsumer(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task Consume(ConsumeContext<GetRootCategoriesRequest> context)
        {
            var categoriesDto = await _categoryRepository.GetRoot(context.Message.UserSettings);
            await context.RespondAsync(new GetRootCategoriesResponse { Categories = categoriesDto });
        }
    }
}
