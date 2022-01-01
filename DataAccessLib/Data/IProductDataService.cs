using DataAccessLib.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IProductDataService
    {
        ProductsWithMetadataModel GetFilteredWithMetadata(ExpandoObject dynamicFilter);
        DetailedProductModel GetOneDetailed(int productId);
        Task<List<ProductModel>> SearchProducts(string searchSentence, int numberOfRecords);

    }
}
