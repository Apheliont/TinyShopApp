using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace TinyShop.Web.Models
{
    public abstract class DynamicFilterModel
    {
        public virtual bool HasProperty(string propName)
        {
            if (string.IsNullOrWhiteSpace(propName))
            {
                throw new ArgumentNullException(nameof(propName));
            }

            return GetType().GetProperty(propName) != null;
        }

        public string DescriptionAttr(string source)
        {
            var member = GetType().GetMember(source).FirstOrDefault();
            var descriptionAttribute =
                member == null
                    ? default(DescriptionAttribute)
                    : member.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return
                descriptionAttribute == null
                    ? source
                    : descriptionAttribute.Description;
        }
    }
}
