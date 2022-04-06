using System.Threading.Tasks;

namespace TinyShop.Web.Services
{
    public interface IUserService
    {
        Task<string> GetLoggedInUserId();
    }
}
