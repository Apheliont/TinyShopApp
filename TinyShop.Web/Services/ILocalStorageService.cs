using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public interface ILocalStorageService
    {
        Task AddToCart(PurchaseModel purchase);
        Task<bool> DeletePurchase(int purchaseId);
        Task<int> GetItemsCountInCart();
        Task<int> GetPurchaseQuantityInCart(int purchaseId);
        Task<List<PurchaseModel>> GetPurchases();
        Task<string> ReadSortingParameters();
        Task SetItemAsync<T>(string name, T value);
        Task<bool> UpdatePurchase(int purchaseId, int qauntity);
    }
}