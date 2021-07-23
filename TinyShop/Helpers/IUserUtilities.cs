using System.Threading.Tasks;

namespace TinyShop.Helpers
{
    public interface IUserUtilities
    {
        Task<string> GetLoggedInUserId();
    }
}