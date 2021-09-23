
using AutoMapper;
using DataAccessLib.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace TinyShop.Helpers;
public static class MetadataUtils
{
    public static DetailsFilterModel CreateDetailsFilter(ExpandoObject obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        // Getting @string name of what DetailsMetadatamodel to instantiate
        string modelToInstatiate = (string)obj
                                    .Where(kvp => kvp.Key == "DetailsFilterModelName")
                                    .First()
                                    .Value;
        // Getting Type of particulare DetailsMetadatamodel
        Type objTypeToInstantiate = Type.GetType($"DataAccessLib.Models.{modelToInstatiate}, DataAccessLib");
        // Creating instance of particulare DetailsMetadatamodel
        DetailsFilterModel DetailsFilterModel = Activator.CreateInstance(objTypeToInstantiate) as DetailsFilterModel;

        var configuration = new MapperConfiguration(cfg => { });
        var mapper = configuration.CreateMapper();
        // Preparing data source. Data source is an ExpandoObject, but we need strogly typed one
        var source = (IDictionary<string, object>)obj;
        // Mapping data from expando object to statically typed one
        var result = mapper.Map(source, DetailsFilterModel, source.GetType(), objTypeToInstantiate);
        return result as DetailsFilterModel;
    }
}
