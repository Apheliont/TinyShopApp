using DataAccessLib.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public class ProductDataService : IProductDataService
    {
        private readonly IProductSqlDataService _sqlDataService;
        private readonly IRequestClient<ProductSearchRequestModel> _searchClient;
        public ProductDataService(
            IProductSqlDataService sqlDataService,
            IRequestClient<ProductSearchRequestModel> searchClient
            )
        {
            _sqlDataService = sqlDataService;
            _searchClient = searchClient;
        }
        public ProductsWithMetadataModel GetFilteredWithMetadata(ExpandoObject dynamicFilter)
        {
            return _sqlDataService.GetFilteredWithMetadata(dynamicFilter);
        }

        public DetailedProductModel GetOneDetailed(int productId)
        {
            return _sqlDataService.GetOneDetailed(productId);
        }

        public async Task<List<ProductModel>> SearchProducts(ProductSearchRequestModel requestModel, CancellationToken token)
        {
            var response = await _searchClient.GetResponse<ProductSearchResponseModel>(requestModel, token);
            List<int> productIds = response.Message.ProductIds;
            if (productIds.Count == 0)
            {
                return new List<ProductModel>();
            }
            return _sqlDataService.GetManyByIds(productIds);
        }
    }
}
