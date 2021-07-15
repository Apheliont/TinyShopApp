using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public class ProductSqlDataService : IProductSqlDataService
    {
        private readonly ISqlDataAccess _dataAccess;
        public ProductSqlDataService(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<ProductModel>> GetRangeByCategory(int categoryId, int from, int to)
        {
            return await _dataAccess.GetData<ProductModel, dynamic>("dbo.spProducts_GetRangeByCategory", new
            {
                CategoryId = categoryId,
                From = from,
                To = to
            });
        }
    }
}
