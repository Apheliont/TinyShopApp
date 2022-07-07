using MassTransit;
using TinyShop.Catalog.Repositories;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Consumers
{
    public class GetBreadcrumbsConsumer : IConsumer<GetBreadcrumbsRequest>
    {
        private readonly IBreadcrumbsRepository _breadcrumbsRepository;
        public GetBreadcrumbsConsumer(IBreadcrumbsRepository breadcrumbsRepository)
        {
            _breadcrumbsRepository = breadcrumbsRepository;
        }
        public async Task Consume(ConsumeContext<GetBreadcrumbsRequest> context)
        {
            var dtos = await _breadcrumbsRepository.Get(context.Message);
            await context.RespondAsync(new GetBreadcrumbsResponse { Breadcrumbs = dtos });
        }
    }
}
