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
                config.CreateMap<CategoryFilter, CategoryFilterDto>()
                        .ForMember(x => x.Result, opt => opt.Ignore());
            });
            return mappingConfig;
        }
    }
}
