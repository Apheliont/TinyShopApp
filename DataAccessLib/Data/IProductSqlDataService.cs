using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IProductSqlDataService
    {
        ProductsWithMetadataModel GetFilteredWithMetadata(ProductFilterModel filterModel);
        Task<ProductMetadataModel> GetMetadata(ProductFilterModel filterModel);
        Task<ProductModel> GetOneDetailed(int productId);
    }
}