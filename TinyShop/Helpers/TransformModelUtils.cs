using AutoMapper;
using DataAccessLib.Enums;
using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace TinyShop.Helpers;

public static class TransformModelUtils
{
    public static void FillupFilterModel(ProductMetadataModel metadata, ProductFilterModel filter)
    {
        if (metadata is null || filter is null)
        {
            throw new ArgumentNullException();
        }

        filter.Price.LowerBound = metadata.Price.LowerBound;
        filter.Price.UpperBound = metadata.Price.UpperBound;
        filter.Price.Measurement = metadata.Price.Measurement;

        if (filter.DetailsFilterModel is not null || metadata.Details is null) {
            return;
        }

        // Getting @string name of what DetailsMetadatamodel to instantiate
        string modelToInstatiate = (string)metadata.Details
                                    .Where(kvp => kvp.Key == "DetailsFilterModelName")
                                    .First()
                                    .Value;
        // Getting Type of particulare DetailsMetadatamodel
        Type objTypeToInstantiate = Type.GetType($"DataAccessLib.Models.{modelToInstatiate}, DataAccessLib");
        // Creating instance of particulare DetailsMetadatamodel
        DetailsFilterModel DetailsFilterModel = Activator.CreateInstance(objTypeToInstantiate) as DetailsFilterModel;

        var configuration = new MapperConfiguration(cfg => {
            cfg
            .CreateMap<object, CheckboxListType>()
            .ConvertUsing(s => new CheckboxListType(((List<object>)s)
                .Select(el => el.ToString()).ToList()));
        });
        var mapper = configuration.CreateMapper();
        // Preparing data source. Data source is an ExpandoObject, but we need strogly typed one
        var source = (IDictionary<string, object>)metadata.Details;
        // Mapping data from expando object to statically typed one
        var result = mapper.Map(source, DetailsFilterModel, source.GetType(), objTypeToInstantiate);
        filter.DetailsFilterModel =  result as DetailsFilterModel;
    }

    public static ExpandoObject CreateFilterDto(ProductFilterModel filterModel)
    {
        if (filterModel == null)
        {
            throw new ArgumentNullException(nameof(filterModel));
        }
        ExpandoObject outerWrapper = new ExpandoObject();
        ExpandoObject details = new ExpandoObject();

        FillUpFilterDto(filterModel, outerWrapper, details);

        if (details.Any())
        {
            outerWrapper.TryAdd("JsonFilterData", JsonConvert.SerializeObject(details));
        }
        return outerWrapper;
    }

    private static void FillUpFilterDto(object objToTraverse, ExpandoObject outerWrapper, ExpandoObject details)
    {
        foreach (PropertyInfo prop in objToTraverse.GetType().GetProperties())
        {
            switch (prop.PropertyType)
            {
                case Type t when t == typeof(RangeType):
                    RangeType sourceRange = (RangeType)prop.GetValue(objToTraverse, null);
                    ExpandoObject destRange = new ExpandoObject();
                    if (sourceRange.From != null && sourceRange.From != sourceRange.LowerBound)
                    {
                        destRange.TryAdd("From", sourceRange.From);
                    }
                    if (sourceRange.To != null && sourceRange.To != sourceRange.UpperBound)
                    {
                        destRange.TryAdd("To", sourceRange.To);
                    }
                    if (destRange.Any())
                    {
                        details.TryAdd(prop.Name, destRange);
                    }                   
                    break;

                case Type t when t == typeof(RowsPerPageEnum) || t == typeof(OrderByEnum) || t == typeof(SortOrderEnum):
                    var sourceEnum = prop.GetValue(objToTraverse, null);
                    outerWrapper.TryAdd(prop.Name, (int)sourceEnum);
                    break;

                case Type t when t == typeof(CheckboxListType):
                    CheckboxListType checkboxList = (CheckboxListType)prop.GetValue(objToTraverse, null);
                    List<string> checkedItems = checkboxList.GetCheckedItems();
                    if (checkedItems.Any())
                    {
                        details.TryAdd(prop.Name, checkedItems);
                    }
                    break;

                case Type t when t == typeof(CheckboxType):
                    CheckboxType checkbox = (CheckboxType)prop.GetValue(objToTraverse, null);
                    if (checkbox.IsChecked)
                    {
                        details.TryAdd(prop.Name, checkbox.IsChecked);
                    }
                    break;

                case Type t when t == typeof(RatingType):
                    RatingType sourceRating = (RatingType)prop.GetValue(objToTraverse, null);
                    if (sourceRating.CurrentRating > 0)
                    {
                        details.TryAdd(prop.Name, sourceRating.CurrentRating);
                    }
                    break;

                case Type t when t == typeof(DetailsFilterModel):
                    // recursion starts here
                    if (prop.GetValue(objToTraverse, null) is not null)
                    {
                        FillUpFilterDto(prop.GetValue(objToTraverse, null), outerWrapper, details);
                    }
                    break;

                default:
                    outerWrapper.TryAdd(prop.Name, prop.GetValue(objToTraverse, null));
                    break;
            }
        }
    }
}
