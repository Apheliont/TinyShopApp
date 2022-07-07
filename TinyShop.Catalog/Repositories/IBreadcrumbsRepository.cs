using TinyShop.Catalog.DTOs;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Repositories
{
    public interface IBreadcrumbsRepository
    {
        Task<List<BreadcrumbDto>> Get(GetBreadcrumbsRequest request);
    }
}
