using AutoMapper;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Entities;

namespace TinyShop.Catalog
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<Image, ImageDto>();
                config.CreateMap<Category, CategoryDto>();
            });
            return mappingConfig;
        }
    }
}
