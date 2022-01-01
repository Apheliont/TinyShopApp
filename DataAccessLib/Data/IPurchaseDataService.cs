using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyShop.Helpers
{
    public interface IPurchaseDataService
    {
        Task<int> AddToCart(PurchaseModel purchase);
        Task<bool> Delete(int purchaseId);
        Task<List<PurchaseModel>> GetAll();
        Task<int> GetItemsCountInCart();
        Task<bool> Update(int purchaseId, int quantity);
    }
}