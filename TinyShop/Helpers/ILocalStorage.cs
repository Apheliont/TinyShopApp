using System.Threading.Tasks;

namespace TinyShop.Helpers
{
    public interface ILocalStorage
    {
        Task<string> ReadSortingParameters();
        Task SetItemAsync<T>(string name, T value);
    }
}