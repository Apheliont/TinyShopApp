using DataAccessLib.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public class ProductDataService : IProductDataService
    {
        private readonly IProductSqlDataService _sqlDataService;
        private readonly IProductElasticService _elasticService;
        public ProductDataService(IProductSqlDataService sqlDataService, IProductElasticService elasticService)
        {
            _elasticService = elasticService;
            _sqlDataService = sqlDataService;
        }
        public ProductsWithMetadataModel GetFilteredWithMetadata(ExpandoObject dynamicFilter)
        {
            return _sqlDataService.GetFilteredWithMetadata(dynamicFilter);
        }

        public DetailedProductModel GetOneDetailed(int productId)
        {
            return _sqlDataService.GetOneDetailed(productId);
        }

        public async Task<List<ProductModel>> SearchProducts(string searchSentence, int numberOfRecords)
        {
            List<int> productIds = await _elasticService.SearchProducts(searchSentence, numberOfRecords);
            if (productIds.Count == 0)
            {
                return new List<ProductModel>();
            }
            return _sqlDataService.GetManyByIds(productIds);
        }
    }
}
