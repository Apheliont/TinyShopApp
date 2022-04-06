using AutoMapper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using TinyShop.Web.CustomTypes;
using TinyShop.Web.DTOs;
using TinyShop.Web.Services;
using TinyShop.Web.Models;

namespace TinyShop.Web.Helpers;

public class MapModelService : IMapModelService
{
    private readonly IMapper _mapper;
    public MapModelService(IMapper mapper)
    {
        _mapper = mapper;
    }
    public void FillupFilterModel(ProductMetadataModel metadata, ProductFilterModel filter)
    {
        if (metadata is null)
        {
            throw new ArgumentNullException();
        }
        filter = new ProductFilterModel();

        filter.Price.LowerBound = metadata.Price.LowerBound;
        filter.Price.UpperBound = metadata.Price.UpperBound;
        filter.Price.Measurement = metadata.Price.Measurement;

        if (
            filter.DynamicFilter is not null
            || metadata.Details is null
            || ((ICollection<KeyValuePair<string, Object>>)metadata.Details).Count == 0
            )
        {
            return;
        }

        // Getting @string name of what DetailsMetadatamodel to instantiate
        string modelToInstatiate = (string)metadata.Details
                                    .Where(kvp => kvp.Key == "DetailsFilterModelName")
                                    .First()
                                    .Value;
        // Getting Type of particulare DetailsMetadatamodel
        Type objTypeToInstantiate = Type.GetType($"{AppDomain.CurrentDomain.FriendlyName}.Models.{modelToInstatiate}, {AppDomain.CurrentDomain.FriendlyName}");
        // Creating instance of particulare DetailsMetadatamodel
        DynamicFilterModel DynamicFilterModel = Activator.CreateInstance(objTypeToInstantiate) as DynamicFilterModel;

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg
            .CreateMap<object, CheckboxListModel>()
            .ConvertUsing(s => new CheckboxListModel(((List<object>)s)
                .Select(el => el.ToString()).ToList()));
        });
        var mapper = configuration.CreateMapper();
        // Preparing data source. Data source is an ExpandoObject, but we need strogly typed one
        var source = (IDictionary<string, object>)metadata.Details;
        // Mapping data from expando object to statically typed one
        var result = mapper.Map(source, DynamicFilterModel, source.GetType(), objTypeToInstantiate);
        filter.DynamicFilter = result as DynamicFilterModel;
    }

    public ExpandoObject CreateFilterDto(ProductFilterModel filterModel)
    {
        if (filterModel == null)
        {
            throw new ArgumentNullException(nameof(filterModel));
        }

        FillUpFilterDto(filterModel, out ExpandoObject outerWrapper);

        return outerWrapper;
    }

    private void FillUpFilterDto(object filterModel, out ExpandoObject outerWrapper)
    {
        outerWrapper = new ExpandoObject();
        foreach (PropertyInfo prop in filterModel.GetType().GetProperties())
        {
            switch (prop.PropertyType)
            {
                case Type t when t == typeof(RangeModel):
                    {
                        RangeModel value = (RangeModel)prop.GetValue(filterModel, null);
                        RangeDtoOut dto = _mapper.Map<RangeDtoOut>(value);
                        ExpandoObject expando = new ExpandoObject();
                        expando.TryAdd("Type", nameof(RangeModel));
                        expando.TryAdd("Data", dto);
                        outerWrapper.TryAdd(prop.Name, expando);
                        break;
                    }


                case Type t when t == typeof(RowsPerPageEnum):
                    {
                        int value = (int)prop.GetValue(filterModel, null);
                        ExpandoObject expando = new ExpandoObject();
                        expando.TryAdd("Type", nameof(RowsPerPageEnum));
                        expando.TryAdd("Data", value);
                        outerWrapper.TryAdd(prop.Name, expando);
                        break;
                    }


                case Type t when t == typeof(OrderByEnum):
                    {
                        var value = prop.GetValue(filterModel, null);
                        ExpandoObject expando = new ExpandoObject();
                        expando.TryAdd("Type", nameof(OrderByEnum));
                        expando.TryAdd("Data", ((OrderByEnum)value).ToString());
                        outerWrapper.TryAdd(prop.Name, expando);
                        break;
                    }


                case Type t when t == typeof(SortOrderEnum):
                    {
                        var value = prop.GetValue(filterModel, null);
                        ExpandoObject expando = new ExpandoObject();
                        expando.TryAdd("Type", nameof(SortOrderEnum));
                        expando.TryAdd("Data", ((SortOrderEnum)value).ToString());
                        outerWrapper.TryAdd(prop.Name, expando);
                        break;
                    }



                case Type t when t == typeof(CheckboxListModel):
                    {
                        CheckboxListModel value = (CheckboxListModel)prop.GetValue(filterModel, null);
                        List<string> checkedItems = value.GetCheckedItems();

                        if (checkedItems.Any())
                        {
                            ExpandoObject expando = new ExpandoObject();
                            expando.TryAdd("Type", nameof(CheckboxListModel));
                            expando.TryAdd("Data", checkedItems);
                            outerWrapper.TryAdd(prop.Name, expando);
                        }
                        break;
                    }


                case Type t when t == typeof(CheckboxModel):
                    {
                        CheckboxModel checkbox = (CheckboxModel)prop.GetValue(filterModel, null);
                        if (checkbox.IsChecked)
                        {
                            ExpandoObject expando = new ExpandoObject();
                            expando.TryAdd("Type", nameof(CheckboxModel));
                            expando.TryAdd("Data", prop.Name);
                            outerWrapper.TryAdd(prop.Name, expando);
                        }
                        break;
                    }


                case Type t when t == typeof(RatingModel):
                    {
                        RatingModel sourceRating = (RatingModel)prop.GetValue(filterModel, null);
                        if (sourceRating.CurrentRating > 0)
                        {
                            ExpandoObject expando = new ExpandoObject();
                            expando.TryAdd("Type", nameof(RatingModel));
                            expando.TryAdd("Data", sourceRating.CurrentRating);
                            outerWrapper.TryAdd(prop.Name, expando);
                        }
                        break;
                    }


                case Type t when t == typeof(DynamicFilterModel):
                    // recursion starts here
                    if (prop.GetValue(filterModel, null) is not null)
                    {
                        FillUpFilterDto(prop.GetValue(filterModel, null), out outerWrapper);
                    }
                    break;

                case Type t when t == typeof(int):
                    {
                        ExpandoObject expando = new ExpandoObject();
                        expando.TryAdd("Type", "Integer");
                        expando.TryAdd("Data", (int)prop.GetValue(filterModel, null));
                        outerWrapper.TryAdd(prop.Name, expando);
                        break;
                    }


                case Type t when t == typeof(double):
                    {
                        ExpandoObject expando = new ExpandoObject();
                        expando.TryAdd("Type", "Double");
                        expando.TryAdd("Data", (double)prop.GetValue(filterModel, null));
                        outerWrapper.TryAdd(prop.Name, expando);
                        break;
                    }


                default:
                    {
                        ExpandoObject expando = new ExpandoObject();
                        expando.TryAdd("Type", prop.GetType().Name);
                        expando.TryAdd("Data", prop.GetValue(filterModel, null));
                        outerWrapper.TryAdd(prop.Name, expando);
                        break;
                    }

            }
        }
    }
}
