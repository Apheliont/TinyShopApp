using TinyShop.ProductSearch.Models;

namespace TinyShop.ProductSearch.Data
{
    public interface IProductService
    {
        Task<List<int>> SearchProducts(ProductSearchRequestModel requestModel);
    }
}
