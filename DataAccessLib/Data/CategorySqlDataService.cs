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

        public async Task<List<CategoryModel>> GetAll()
        {
            return await _dataAccess.GetData<CategoryModel, dynamic>("spCategories_GetAll", new { });
        }
    }
}
