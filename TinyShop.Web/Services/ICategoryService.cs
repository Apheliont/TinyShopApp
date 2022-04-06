using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetRoot();
        Task<List<CategoryModel>> GetSubcategories(int categoryId);
    }
}
