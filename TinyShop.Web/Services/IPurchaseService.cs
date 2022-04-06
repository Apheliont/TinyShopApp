using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public interface IPurchaseService
    {
        Task<int> AddToCart(PurchaseModel purchase);
        Task<bool> Delete(int purchaseId);
        Task<List<PurchaseModel>> GetAll();
        Task<int> GetItemsCountInCart();
        Task<bool> Update(int purchaseId, int quantity);
    }
}
