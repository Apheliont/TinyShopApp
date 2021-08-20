using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
namespace TinyShop.Helpers
{
    public class LocalStorage : ILocalStorage
    {
        private ILocalStorageService _localStorage;
        public LocalStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> ReadSortingParameters()
        {
            StringBuilder sb = new StringBuilder();

            if (await _localStorage.ContainKeyAsync("orderBy"))
            {
                sb.Append($"&orderBy={await _localStorage.GetItemAsync<int>("orderBy")}");
            }
            if (await _localStorage.ContainKeyAsync("sortOrder"))
            {
                sb.Append($"&sortOrder={await _localStorage.GetItemAsync<int>("sortOrder")}");
            }
            if (await _localStorage.ContainKeyAsync("rowsPerPage"))
            {
                sb.Append($"&rowsPerPage={await _localStorage.GetItemAsync<int>("rowsPerPage")}");
            }
            return sb.ToString();
        }

        public async Task SetItemAsync<T>(string name, T value)
        {
            await _localStorage.SetItemAsync(name, value);
        }
    }
}
