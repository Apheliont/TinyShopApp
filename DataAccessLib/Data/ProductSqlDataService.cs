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

        public ProductsWithMetadataModel GetFilteredWithMetadata(ProductFilterModel filterModel)
        {
            // TODO: Flatten filterModel to suitable as a parameter for sql
            var param = new {
                PageNumber = filterModel.PageNumber,
                OrderBy = filterModel.OrderBy,
                MinPrice = filterModel.Price.From is not null ? filterModel.Price.From : 0,
                MaxPrice = filterModel.Price.To is not null ? filterModel.Price.To : 99999,
                SortOrder = filterModel.SortOrder,
                CategoryId = filterModel.CategoryId,
                RowsPerPage = filterModel.RowsPerPage
            };

                string jsonText = _dataAccess
                        .GetJsonText<dynamic>("spProducts_GetFilteredWithMetadata", param);
                return JsonConvert.DeserializeObject<ProductsWithMetadataModel>(jsonText);
        }

        public DetailedProductModel GetOneDetailed(int productId)
        {
            string jsonText = _dataAccess
                .GetJsonText<dynamic>("spProducts_GetOneDetailed", new { ProductId = productId });
            return JsonConvert.DeserializeObject<DetailedProductModel>(jsonText);
        }
    }
}
