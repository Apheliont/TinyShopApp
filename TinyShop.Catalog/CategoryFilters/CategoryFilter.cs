using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinyShop.Catalog.CategoryFilters
{
    public abstract class CategoryFilter
    {
        public ExpandoObject Build()
        {
            ExpandoObject result = new ExpandoObject();

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                ExpandoObject innerObject = new ExpandoObject();
                string propName = prop.Name;
                var attributes = prop.GetCustomAttributes(typeof(FilterAttribute), false);
                if (attributes.Any())
                {
                    var descriptionAttribute = (FilterAttribute)attributes.First();
                    innerObject.TryAdd("Description", descriptionAttribute.Description);
                    innerObject.TryAdd("Unit", descriptionAttribute.Unit);
                }
                else
                {
                    innerObject.TryAdd("Description", propName);
                }

                innerObject.TryAdd("Type", prop.PropertyType.Name);

                result.TryAdd(propName, innerObject);
            }

            return result;
        }
    }
}
