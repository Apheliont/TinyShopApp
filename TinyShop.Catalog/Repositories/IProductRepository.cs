using System.Dynamic;
using TinyShop.Catalog.DTOs;

namespace TinyShop.Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<ProductsInfoDto> FilterProducts(ProductFilterDto productFilter);
        Task<ProductsInfoDto> GetProductsAndInfo(ProductFilterDto productFilter);
        Task<ProductDto> GetProduct(int productId);
        Task<List<ProductDto>> GetProducts(int[] ids);
    }
}
