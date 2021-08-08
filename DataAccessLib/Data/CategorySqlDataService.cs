using DataAccessLib.DataAccess;
using DataAccessLib.Models;
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


        public async Task<List<CategoryModel>> GetRoot()
        {
            return await Task.Run(() =>
            {
                return _dataAccess
                .GetWithNestedObjectData<CategoryModel, ImageModel, dynamic>
                    (
                        "spCategories_GetRoot", "Image", new { }
                    );
            });

        }

        public async Task<List<CategoryModel>> GetSubcategories(int categoryId)
        {
            return await Task.Run(() =>
            {
                return _dataAccess
                .GetWithNestedObjectData<CategoryModel, ImageModel, dynamic>
                    (
                        "spCategories_GetSubcategories", "Image", new { CategoryId = categoryId }
                    );
            });

        }
    }
}
