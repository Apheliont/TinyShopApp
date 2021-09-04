using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyShop.Helpers
{
    public interface IUriUtils
    {
        List<BreadcrumbItemModel> GetBreadcrumbs(string uri);
    }
}