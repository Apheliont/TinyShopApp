using System.Dynamic;
using TinyShop.Catalog.DTOs;

namespace TinyShop.Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<ProductsInfoDto> FilterProducts(ExpandoObject dynamicFilter);
        Task<ProductDto> GetProduct(int productId);
        Task<List<ProductDto>> GetProducts(int[] ids);
    }
}
