using DataAccessLib.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IProductDataService
    {
        ProductsWithMetadataModel GetFilteredWithMetadata(ExpandoObject dynamicFilter);
        DetailedProductModel GetOneDetailed(int productId);
        Task<List<ProductModel>> SearchProducts(ProductSearchRequestModel requestModel, CancellationToken token);
    }
}
