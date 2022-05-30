using TinyShop.Catalog.DTOs;
using Microsoft.EntityFrameworkCore;
using TinyShop.Catalog.Extensions;
using TinyShop.Catalog.Entities;
using AutoMapper;
using System.Linq.Dynamic.Core;


namespace TinyShop.Catalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductsInfoDto> FilterProducts(ProductFilterDto productFilter)
        {

            return await _db
                .Products
                .Include(p => p.Category)
                .FilterProducts(productFilter, _mapper);
        }

        public async Task<ProductDto> GetProduct(int productId)
        {
            Product? product = await _db.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
            if (product is not null)
            {
                return _mapper.Map<ProductDto>(product);
            }
            return new ProductDto();
        }

        public async Task<List<ProductDto>> GetProducts(int[] ids)
        {
            List<Product> products = await _db.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductsInfoDto> GetProductsAndInfo(ProductFilterDto productFilter)
        {
            return await _db
                .Products
                .Include(p => p.Category).ThenInclude(c => c.CategoryFilters)
                .GetProductsAndInfo(productFilter, _mapper);
        }
    }
}
