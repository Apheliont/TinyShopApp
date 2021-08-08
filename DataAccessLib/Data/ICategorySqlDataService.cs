using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface ICategorySqlDataService
    {
        Task<List<CategoryModel>> GetRoot();
        Task<List<CategoryModel>> GetSubcategories(int categoryId);
    }
}