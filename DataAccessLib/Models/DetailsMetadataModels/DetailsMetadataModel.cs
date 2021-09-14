using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DataAccessLib.Models
{
    public abstract class DetailsMetadataModel
    {
        public virtual bool HasProperty(string propName)
        {
            if (string.IsNullOrWhiteSpace(propName))
            {
                throw new ArgumentNullException(nameof(propName));
            }

            return this.GetType().GetProperty(propName) != null;
        }
    }
}
