using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public class CategorySqlDataService : ICategorySqlDataService
    {
        private readonly ISqlDataAccess _dataAccess;
        public CategorySqlDataService(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }


        public List<CategoryModel> GetRoot()
        {
            string jsonText = _dataAccess
                        .GetJsonText<dynamic>(
                        "spCategories_GetRoot", new { });
            return JsonConvert.DeserializeObject<List<CategoryModel>>(jsonText);
        }

        public List<CategoryModel> GetSubcategories(int categoryId)
        {
            string jsonText = _dataAccess
                        .GetJsonText<dynamic>(
                        "spCategories_GetSubcategories", new { CategoryId = categoryId });
            return JsonConvert.DeserializeObject<List<CategoryModel>>(jsonText);
        }
    }
}
