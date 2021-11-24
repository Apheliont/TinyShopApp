using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public ProductsWithMetadataModel GetFilteredWithMetadata(ExpandoObject dynamicFilter)
        {

            string jsonText = _dataAccess
                        .GetJsonText<dynamic>("spProducts_GetFilteredWithMetadata", dynamicFilter);
                return JsonConvert.DeserializeObject<ProductsWithMetadataModel>(jsonText);
        }

        public DetailedProductModel GetOneDetailed(int productId)
        {
            string jsonText = _dataAccess
                .GetJsonText<dynamic>("spProducts_GetOneDetailed", new { ProductId = productId });
            return JsonConvert.DeserializeObject<DetailedProductModel>(jsonText);
        }

        public List<ProductModel> SearchProducts(string searchSentence, int numberOfRecords)
        {

            string jsonText = _dataAccess
                        .GetJsonText<dynamic>("spProducts_Search", new { 
                            SearchSentence = searchSentence,
                            NumberOfRecords = numberOfRecords
                        });
            return JsonConvert.DeserializeObject<List<ProductModel>>(jsonText);
        }
    }
}
