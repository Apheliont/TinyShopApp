
namespace TinyShop.Catalog.CategoryFilters
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterAttribute : Attribute
    {
        private string _description = "";

        private string _unit = "";

        public FilterAttribute(string description, string unit)
        {
            _description = description;
            _unit = unit;
        }

        public FilterAttribute(string description)
        {
            _description = description;
        }

        public string Description
        {
            get { return _description; }
        }

        public string Unit
        {
            get { return _unit; }
        }
    }
}
