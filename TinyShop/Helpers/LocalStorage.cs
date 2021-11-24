using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DataAccessLib.Models;
using Newtonsoft.Json;

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

            if (await _localStorage.ContainKeyAsync("OrderBy"))
            {
                sb.Append($"&OrderBy={await _localStorage.GetItemAsync<int>("OrderBy")}");
            }
            if (await _localStorage.ContainKeyAsync("SortOrder"))
            {
                sb.Append($"&SortOrder={await _localStorage.GetItemAsync<int>("SortOrder")}");
            }
            if (await _localStorage.ContainKeyAsync("rowsPerPage"))
            {
                sb.Append($"&RowsPerPage={await _localStorage.GetItemAsync<int>("RowsPerPage")}");
            }
            return sb.ToString();
        }

        public async Task AddToCart(PurchaseModel purchase)
        {
            List<PurchaseModel> shoppingCart;
            if (await _localStorage.ContainKeyAsync("shoppingCart"))
            {
                string cartStringData = await _localStorage.GetItemAsync<string>("shoppingCart");
                shoppingCart = JsonConvert.DeserializeObject<List<PurchaseModel>>(cartStringData);
                PurchaseModel purchaseInLocalStorage = shoppingCart.Find(p => p.Id == purchase.Id);
                if (purchaseInLocalStorage is not null)
                {
                    purchaseInLocalStorage.Quantity += 1;
                } else
                {
                    shoppingCart.Add(purchase);
                }
            } else
            {
                shoppingCart = new List<PurchaseModel>();
                shoppingCart.Add(purchase);
            }
            await _localStorage.SetItemAsync("shoppingCart", JsonConvert.SerializeObject(shoppingCart));
        }

        public async Task<List<PurchaseModel>> GetPurchases()
        {
            List<PurchaseModel> shoppingCart;
            if (await _localStorage.ContainKeyAsync("shoppingCart"))
            {
                string cartStringData = await _localStorage.GetItemAsync<string>("shoppingCart");
                shoppingCart = JsonConvert.DeserializeObject<List<PurchaseModel>>(cartStringData);
            }
            else
            {
                shoppingCart = new List<PurchaseModel>();
            }
            return shoppingCart;
        }

        public async Task<bool> DeletePurchase(int purchaseId)
        {
            if (await _localStorage.ContainKeyAsync("shoppingCart"))
            {
                string cartStringData = await _localStorage.GetItemAsync<string>("shoppingCart");
                List<PurchaseModel> shoppingCart = JsonConvert.DeserializeObject<List<PurchaseModel>>(cartStringData);
                if (shoppingCart.Remove(shoppingCart.Where(p => p.Id == purchaseId).FirstOrDefault()))
                {
                    await _localStorage.SetItemAsync("shoppingCart", JsonConvert.SerializeObject(shoppingCart));
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdatePurchase(int purchaseId, int qauntity)
        {
            if (await _localStorage.ContainKeyAsync("shoppingCart"))
            {
                string cartStringData = await _localStorage.GetItemAsync<string>("shoppingCart");
                List<PurchaseModel> shoppingCart = JsonConvert.DeserializeObject<List<PurchaseModel>>(cartStringData);
                PurchaseModel purchaseInLocalStorage = shoppingCart.Find(p => p.Id == purchaseId);
                if (purchaseInLocalStorage is not null)
                {
                    purchaseInLocalStorage.Quantity = qauntity;
                    await _localStorage.SetItemAsync("shoppingCart", JsonConvert.SerializeObject(shoppingCart));
                    return true;
                }
            }
            return false;
        }

        public async Task<int> GetItemsCountInCart()
        {
            if (await _localStorage.ContainKeyAsync("shoppingCart"))
            {
                string cartStringData = await _localStorage.GetItemAsync<string>("shoppingCart");
                List<PurchaseModel> shoppingCart = JsonConvert.DeserializeObject<List<PurchaseModel>>(cartStringData);
                return shoppingCart.Sum(p => p.Quantity);
            }
            return 0;
        }

        public async Task SetItemAsync<T>(string name, T value)
        {
            await _localStorage.SetItemAsync(name, value);
        }
    }
}
