using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface ICategorySqlDataService
    {
        List<CategoryModel> GetRoot();
        List<CategoryModel> GetSubcategories(int categoryId);
    }
}