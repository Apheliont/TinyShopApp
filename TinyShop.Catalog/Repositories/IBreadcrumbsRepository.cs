using TinyShop.Catalog.DTOs;

namespace TinyShop.Catalog.Repositories
{
    public interface IBreadcrumbsRepository
    {
        Task<List<BreadcrumbDto>> Get(int id, bool isProduct);
    }
}
