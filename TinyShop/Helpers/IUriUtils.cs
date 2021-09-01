using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyShop.Helpers
{
    public interface IUriUtils
    {
        Task<List<CategoryParentModel>> GetParents(string uri);
    }
}