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


        public async Task<List<ProductModel>> GetFiltered(ProductFilterModel filterModel)
        {
            return await _dataAccess
                .GetData<ProductModel, ProductFilterModel>("dbo.spProducts_GetFiltered", filterModel);
        }

        public async Task<ProductMetadataModel> GetMetadata(ProductFilterModel filterModel)
        {
            var data = await _dataAccess
                .GetData<ProductMetadataModel, dynamic>("dbo.spProducts_GetMetadata",
                    new { 
                        CategoryId = filterModel.CategoryId,
                        MinPrice = filterModel.MinPrice,
                        MaxPrice = filterModel.MaxPrice
                    });
            return data.FirstOrDefault();
        }
    }
}
