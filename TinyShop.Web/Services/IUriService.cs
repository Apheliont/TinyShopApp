using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public interface IUriService
    {
        Task<List<BreadcrumbModel>> GetBreadcrumbs(string uri, UserSettings userSettings);
    }
}