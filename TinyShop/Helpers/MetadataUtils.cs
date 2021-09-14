
using AutoMapper;
using DataAccessLib.Models;
using System.Dynamic;

namespace TinyShop.Helpers;
public static class MetadataUtils
{
    public static DetailsMetadataModel CreateDetailsMetadata(ExpandoObject obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        // Getting @string name of what DetailsMetadatamodel to instantiate
        string modelToInstatiate = (string)obj
                                    .Where(kvp => kvp.Key == "DetailsMetadataModelName")
                                    .First()
                                    .Value;
        // Getting Type of particulare DetailsMetadatamodel
        Type objTypeToInstantiate = Type.GetType($"DataAccessLib.Models.{modelToInstatiate}, DataAccessLib");
        // Creating instance of particulare DetailsMetadatamodel
        DetailsMetadataModel DetailsMetadataModel = Activator.CreateInstance(objTypeToInstantiate) as DetailsMetadataModel;

        var configuration = new MapperConfiguration(cfg => { });
        var mapper = configuration.CreateMapper();
        // Preparing data source. Data source is an ExpandoObject, but we need strogly typed one
        var source = (IDictionary<string, object>)obj;
        // Mapping data from expando object to statically typed one
        var result = mapper.Map(source, DetailsMetadataModel, source.GetType(), objTypeToInstantiate);
        return result as DetailsMetadataModel;
    }
}
