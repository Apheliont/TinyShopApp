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


        public async Task<List<ProductModel>> GetFilteredWithMetadata(ProductFilterModel filterModel)
        {
            return await _dataAccess.GetData<ProductModel, ProductFilterModel>("dbo.spProducts_GetFilteredWithMetadata", filterModel);
        }
    }
}
