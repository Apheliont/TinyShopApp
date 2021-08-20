using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<ProductMetadataModel> GetMetadata(ProductFilterModel filterModel)
        {
            var data = await _dataAccess
                .GetData<ProductMetadataModel, dynamic>("dbo.spProducts_GetMetadata",
                    new
                    {
                        CategoryId = filterModel.CategoryId,
                        MinPrice = filterModel.MinPrice,
                        MaxPrice = filterModel.MaxPrice,
                        MinRating = filterModel.MinRating
                    });
            return data.FirstOrDefault();
        }

        public ProductsWithMetadataModel GetFilteredWithMetadata(ProductFilterModel filterModel)
        {
                string jsonText = _dataAccess
                        .GetJsonText<dynamic>(
                        "spProducts_GetFilteredWithMetadata", filterModel);
                return JsonConvert.DeserializeObject<ProductsWithMetadataModel>(jsonText);
        }

        public Task<ProductModel> GetOneDetailed(int productId)
        {
            return Task.Run(() =>
            {
                return _dataAccess
                .GetWithNestedListData<ProductModel, ImageModel, dynamic>(
                    "spProducts_GetOneDetailed", "Images", new { ProductId = productId }
                    ).FirstOrDefault();
            });
        }
    }
}
