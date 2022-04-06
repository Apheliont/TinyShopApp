using AutoMapper;
using TinyShop.Web.DTOs;
using TinyShop.Web.Models;

namespace TinyShop.Web
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, ProductModel>(); // <--|
                config.CreateMap<BreadcrumbDto, BreadcrumbModel>(); // <--|
                config.CreateMap<CategoryDto, CategoryModel>(); // <--|
                //config.CreateMap<ProductFilterModel, ProductFilterDto>() // |-->
                //    .ForMember(dest => dest.RowsPerPage, opt => opt.MapFrom(src => (int)src.RowsPerPage))
                //    .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating.CurrentRating));
                config.CreateMap<ImageDto, ImageModel>(); // <--|
                config.CreateMap<ProductMetadataDto, ProductMetadataModel>(); // <--|
                config.CreateMap<ProductsInfoDto, ProductsInfoModel>(); // <--|
                config.CreateMap<RangeDtoIn, RangeModel>(); // <--|
                config.CreateMap<PurchaseDto, PurchaseModel>(); // <--|
                config.CreateMap<RatingDto, RatingModel>();
                config.CreateMap<RangeModel, RangeDtoOut>()
                    .ForMember(dest => dest.From,
                        opt => opt.MapFrom(src => src.From != null && src.From > src.LowerBound
                        ? src.From : src.LowerBound))
                     .ForMember(dest => dest.To,
                        opt => opt.MapFrom(src => src.To != null && src.To < src.UpperBound
                        ? src.To : src.UpperBound));
            });
        }
    }
}
