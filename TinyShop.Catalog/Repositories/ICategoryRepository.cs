using TinyShop.Catalog.DTOs;

namespace TinyShop.Catalog.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetRoot();
        Task<List<CategoryDto>> GetSubcategories(int categoryId);
    }
}