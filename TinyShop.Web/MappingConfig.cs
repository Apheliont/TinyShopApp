using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TinyShop.Web.DTOs;
using TinyShop.Web.Models;

namespace TinyShop.Web
{
    public class RangeIntOutConverter : ITypeConverter<RangeModel<int>, RangeDto<int>?>
    {
        public RangeDto<int>? Convert(RangeModel<int> source, RangeDto<int>? destination, ResolutionContext context)
        {

            if (source.LowerBound == source.From && source.UpperBound == source.To)
            {
                return null;
            }
            var lowerBound = source.From > source.LowerBound ? source.From : source.LowerBound;
            var upperBound = source.To < source.UpperBound ? source.To : source.UpperBound;
            return new RangeDto<int>() { LowerBound = lowerBound, UpperBound = upperBound };
        }
    }

    public class RangeDoubleOutConverter : ITypeConverter<RangeModel<double>, RangeDto<double>?>
    {
        public RangeDto<double>? Convert(RangeModel<double> source, RangeDto<double>? destination, ResolutionContext context)
        {

            if (source.LowerBound == source.From && source.UpperBound == source.To)
            {
                return null;
            }
            var lowerBound = source.From > source.LowerBound ? source.From : source.LowerBound;
            var upperBound = source.To < source.UpperBound ? source.To : source.UpperBound;
            return new RangeDto<double>() { LowerBound = lowerBound, UpperBound = upperBound };
        }
    }
    public class CategoryInFilterConverter : ITypeConverter<CategoryFilterDto, CategoryFilter>
    {
        public CategoryFilter Convert(CategoryFilterDto source, CategoryFilter destination, ResolutionContext context)
        {
            CategoryFilter categoryFilter = new CategoryFilter
            {
                Description = source.Description,
                Measurement = source.Measurement,
                Index = source.Index ?? 0,
                Name = source.Name,
                Type = source.Type,
            };

            if (source.Result is null) return categoryFilter;

            switch (source.Type.ToLower())
            {
                case "checkbox":
                    {
                        List<string>? items = JsonConvert.DeserializeObject<List<string>>(source.Result.ToString()!);
                        if (items is null) { 
                            categoryFilter.Result = null;
                            break;
                        }
                        categoryFilter.Result = new CheckboxModel(items);
                        break;
                    }

                case "radio":
                    {
                        List<string>? items = JsonConvert.DeserializeObject<List<string>>(source.Result.ToString()!);
                        if (items is null)
                        {
                            categoryFilter.Result = null;
                            break;
                        }
                        categoryFilter.Result = new RadioModel(items);
                        break;
                    }

                case "range<double>":
                    {
                        RangeDto<double>? rangeDto = JsonConvert.DeserializeObject<RangeDto<double>?>(source.Result.ToString()!);
                        if (rangeDto is null)
                        {
                            categoryFilter.Result = null;
                            break;
                        }
                        categoryFilter.Result = context.Mapper.Map<RangeModel<double>>(rangeDto);
                        break;
                    }

                case "range<int>":
                    {
                        RangeDto<int>? rangeDto = JsonConvert.DeserializeObject<RangeDto<int>?>(source.Result.ToString()!);
                        if (rangeDto is null)
                        {
                            categoryFilter.Result = null;
                            break;
                        }
                        categoryFilter.Result = context.Mapper.Map<RangeModel<int>>(rangeDto);
                        break;
                    }
            }
            return categoryFilter;
        }
    }

    public class CategoryOutFilterConverter : ITypeConverter<CategoryFilter, CategoryFilterDto>
    {
        public CategoryFilterDto Convert(CategoryFilter source, CategoryFilterDto destination, ResolutionContext context)
        {
            CategoryFilterDto categoryFilter = new CategoryFilterDto
            {
                Name = source.Name,
                Type = source.Type
            };

            if (source.Result is null) return categoryFilter;

            switch (source.Type.ToLower())
            {
                case "checkbox":
                    {
                        List<string> checkedItems = ((CheckboxModel)source.Result).GetCheckedItems();
                        categoryFilter.Result = checkedItems.Count != 0 ? checkedItems : null;
                        break;
                    }

                case "radio":
                    {
                        string selectedItem = ((RadioModel)source.Result).SelectedItem;
                        if (String.IsNullOrEmpty(selectedItem))
                        {
                            categoryFilter.Result = null;
                        }
                        else
                        {
                            categoryFilter.Result = selectedItem;
                        }
                        break;
                    }

                case "range<double>":
                    {
                        categoryFilter.Result = context.Mapper.Map<RangeDto<double>?>((RangeModel<double>)source.Result);
                        break;
                    }

                case "range<int>":
                    {
                        categoryFilter.Result = context.Mapper.Map<RangeDto<int>?>((RangeModel<int>)source.Result);
                        break;
                    }
            }
            return categoryFilter;
        }
    }

    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, ProductModel>();
                config.CreateMap<BreadcrumbDto, BreadcrumbModel>();
                config.CreateMap<CategoryDto, CategoryModel>();
                config.CreateMap<ImageDto, ImageModel>();
                config.CreateMap<ProductMetadataDto, ProductMetadataModel>();
                config.CreateMap<ProductsInfoDto, ProductsInfoModel>();
                config.CreateMap<PurchaseDto, PurchaseModel>();
                config.CreateMap<RatingDto, RatingModel>();
                config.CreateMap<CategoryFilterDto, CategoryFilter>()
                    .ConvertUsing(new CategoryInFilterConverter());

                config.CreateMap<CategoryFilter, CategoryFilterDto>()
                    .ConvertUsing(new CategoryOutFilterConverter());


                config.CreateMap<RangeModel<double>, RangeDto<double>?>()
                .ConvertUsing(new RangeDoubleOutConverter());

                config.CreateMap<RangeModel<int>, RangeDto<int>?>()
                .ConvertUsing(new RangeIntOutConverter());

                config.CreateMap<RangeDto<double>, RangeModel<double>>()
                    .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.LowerBound))
                     .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.UpperBound));

                config.CreateMap<RangeDto<int>, RangeModel<int>>()
                    .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.LowerBound))
                        .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.UpperBound));


                config.CreateMap<ProductMetadataModel, ProductFilterModel>();
                config.CreateMap<ProductFilterModel, ProductFilterDto>()
                    .ForMember(dest => dest.RowsPerPage, opt => opt.MapFrom(src => (int)src.RowsPerPage))
                    .ForMember(dest => dest.CategoryFilters, opt => opt.MapFrom(src => src.CategoryFilters))
                    .ForMember(dest => dest.Price, opt => opt.Condition(
                        src => src.Price.LowerBound != default || src.Price.UpperBound != default));
            });
        }
    }
}
