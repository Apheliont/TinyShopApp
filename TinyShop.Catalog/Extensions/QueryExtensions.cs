using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyShop.Catalog.CustomTypes;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Entities;
using System.Linq.Dynamic.Core;
using System.Dynamic;

namespace TinyShop.Catalog.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<Product> ApplyDynamicFilter(this IQueryable<Product> query, ExpandoObject dynamicFilter)
        {
            
            //query = query.Skip((dynamicFilter.PageNumber - 1) * dynamicFilter.RowsPerPage).Take(dynamicFilter.RowsPerPage);
            //if (dynamicFilter.Filters is null)
            //    return query;
            if (dynamicFilter is null)
            {
                throw new Exception("Dynamic filter is not initialized");
            }

            foreach (KeyValuePair<string, object?> keyValuePair in dynamicFilter)
            {
                if (keyValuePair.Value is null)
                {
                    throw new Exception("Filter value cannot be null");
                }

                ExpandoObject value = (ExpandoObject)keyValuePair.Value;
                string key = keyValuePair.Key;

                ((IDictionary<string, object?>)value).TryGetValue("Type", out object? dataType);

                switch (dataType?.ToString()?.ToLower())
                {
                    case "orderby":
                        query = query.OrderBy($"{Enum.GetName(typeof(OrderByEnum), value.Data)} {Enum.GetName(typeof(SortOrderEnum), dynamicFilter.SortOrder)}");
                        break;
                }
            }

            return query;
        }
    }
}
