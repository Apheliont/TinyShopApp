using MassTransit;
using TinyShop.Catalog.DTOs;
using Microsoft.EntityFrameworkCore;
using TinyShop.Catalog.Extensions;
using TinyShop.Catalog.Entities;
using TinyShop.Catalog.CustomTypes;
using AutoMapper;
using System.Dynamic;

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
        public async Task<ProductsInfoDto> FilterProducts(ExpandoObject dynamicFilter)
        {
            //IQueryable<Product> query = _db.Products.Include(p => p.Images).ApplyDynamicFilter(dynamicFilter);
            //List<Product> products = await query.ToListAsync();
            //List<ProductDto> productsDto = _mapper.Map<List<ProductDto>>(products);

            //ProductMetadataDto productMetadata = new();
            //productMetadata.FoundRecords = products.Count();
            //productMetadata.Price = new RangeType
            //{
            //    LowerBound = products.Min(p => p.Price),
            //    UpperBound = products.Max(p => p.Price)
            //};
            //return new ProductsInfoDto { Products = productsDto, Metadata = productMetadata };
            return new ProductsInfoDto()
            {
                Metadata = new ProductMetadataDto
                {
                    FoundRecords = 2,
                    Price = new RangeDto
                    {
                        LowerBound = 100,
                        UpperBound = 1000,
                        Measurement = "руб."
                    }
                },
                Products = new List<ProductDto> {
                    new ProductDto {
                        Id = 1,
                        ProductName = "Кофеварка",
                        Price = 9845.45,
                        Description = "Клевая кофеварка бери пока цены норм",
                        Images = new List<ImageDto>() {
                            new ImageDto { UriSizeM = "https://kscom.ru/images/detailed/271/0cc12227-f8d7-11e6-9444-00259080c886_157b01c6-7075-11e7-9455-00259080c886.jpeg", IsMain = true }
                        }
                    },
                    new ProductDto {
                        Id = 2,
                        ProductName = "Колли",
                        Price = 100.54,
                        Description = "Когда-то и у меня была собака...",
                        Images = new List<ImageDto>() {
                            new ImageDto { UriSizeM = "https://zoolog.guru/wp-content/uploads/2019/04/blobid1554118554940.jpg", IsMain = true }
                        }
                    }
                }
            };
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
    }
}
