using AutoMapper;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.Text.Json;

namespace TinyShop.Catalog
{
    public class JDocToExpando : ITypeConverter<JsonDocument, ExpandoObject?>
    {
        public ExpandoObject? Convert(JsonDocument source, ExpandoObject? destination, ResolutionContext context)
        {
            if (source is null) return null;
            return JsonConvert.DeserializeObject<ExpandoObject>(source.RootElement.ToString(),
                                                new ExpandoObjectConverter());
        }
    }

    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<JsonDocument, ExpandoObject?>()
                        .ConvertUsing(new JDocToExpando());
                config.CreateMap<Image, ImageDto>();
                config.CreateMap<Category, CategoryDto>();
                config.CreateMap<ProductTranslation, Product>();
                config.CreateMap<CategoryTranslation, Category>();
                config.CreateMap<CategoryFilterTranslation, CategoryFilter>();
                config.CreateMap<CategoryFilter, CategoryFilterDto>()
                        .ForMember(x => x.Result, opt => opt.Ignore());
                config.CreateMap<Category, BreadcrumbDto>()
                        .ForMember(b => b.ItemName, opt => opt.MapFrom(src => src.CategoryName))
                        .ForMember(b => b.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(b => b.IsProduct, opt => opt.MapFrom(src => false));
                config.CreateMap<Product, BreadcrumbDto>()
                        .ForMember(b => b.ItemName, opt => opt.MapFrom(src => src.ProductName))
                        .ForMember(b => b.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(b => b.IsProduct, opt => opt.MapFrom(src => true));
            });
            return mappingConfig;
        }
    }
}
