using System.Dynamic;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public interface IMapModelService
    {
        public void FillupFilterModel(ProductMetadataModel metadata, ProductFilterModel filter);
        public ExpandoObject CreateFilterDto(ProductFilterModel filterModel);
    }
}
