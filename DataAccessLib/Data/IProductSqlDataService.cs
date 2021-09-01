using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IProductSqlDataService
    {
        ProductsWithMetadataModel GetFilteredWithMetadata(ProductFilterModel filterModel);
        DetailedProductModel GetOneDetailed(int productId);
    }
}