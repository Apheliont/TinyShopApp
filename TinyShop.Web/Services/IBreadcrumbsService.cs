using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public interface IBreadcrumbsService
    {
        Task<List<BreadcrumbModel>> Get(int id, bool isProduct);
    }
}
