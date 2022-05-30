using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public interface IProductService
    {
        Task<ProductsInfoModel> FilterProducts(ProductFilterModel filter);
        Task<ProductsInfoModel> GetProductsAndInfo(ProductFilterModel filter);
        Task<ProductModel> GetProduct(int id);
        Task<List<ProductModel>> GetProducts(int[] ids);
        Task<List<ProductModel>> SearchProducts(string searchSentence, int numberOfRecords, CancellationToken token);
    }
}
