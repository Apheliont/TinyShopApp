using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace DataAccessLib.Models
{
    public abstract class DetailsFilterModel
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
