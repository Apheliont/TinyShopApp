using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IProductSqlDataService
    {
        Task<List<ProductModel>> GetFilteredWithMetadata(ProductFilterModel filterModel);
    }
}