using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Web.DTOs;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetRoot(UserSettings userSettings);
        Task<List<CategoryModel>> GetSubcategories(int categoryId, UserSettings userSettings);
    }
}
