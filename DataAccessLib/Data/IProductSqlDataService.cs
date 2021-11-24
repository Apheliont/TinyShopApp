﻿using DataAccessLib.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IProductSqlDataService
    {
        ProductsWithMetadataModel GetFilteredWithMetadata(ExpandoObject dynamicFilter);
        DetailedProductModel GetOneDetailed(int productId);
        List<ProductModel> SearchProducts(string searchSentence, int numberOfRecords);
    }
}