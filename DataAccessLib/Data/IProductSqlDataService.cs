using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IProductSqlDataService
    {
        Task<List<ProductModel>> GetFiltered(ProductFilterModel filterModel);
        Task<ProductMetadataModel> GetMetadata(ProductFilterModel filterModel);
    }
}