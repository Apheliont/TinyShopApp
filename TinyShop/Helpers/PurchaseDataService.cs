using DataAccessLib.Data;
using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyShop.Helpers
{
    public class PurchaseDataService : IPurchaseDataService
    {
        private readonly IPurchaseSqlDataService _purchaseSqlDataService;
        private readonly IUserUtilities _userUtilities;
        private readonly ILocalStorage _localStorage;
        public PurchaseDataService(
            IPurchaseSqlDataService purchaseSqlDataService,
            IUserUtilities userUtilities,
            ILocalStorage localStorage)
        {
            _purchaseSqlDataService = purchaseSqlDataService;
            _userUtilities = userUtilities;
            _localStorage = localStorage;
        }
        public async Task AddToCart(PurchaseModel purchase)
        {
            string userId = await _userUtilities.GetLoggedInUserId();
            if (userId is not null)
            {
                await _purchaseSqlDataService.AddToCart(userId, purchase.Id, 1);
            }
            else
            {
                await _localStorage.AddToCart(purchase);
            }
        }

        public async Task<bool> Delete(int purchaseId)
        {
            string userId = await _userUtilities.GetLoggedInUserId();
            if (userId is not null)
            {
                // We have to ensure that this particular purchase is indeed belong current user
                // Don't let client side ability to arbitrary delete any purchase 
                List<PurchaseModel> purchases = await _purchaseSqlDataService.GetAll(userId);
                if (purchases.Exists(p => p.Id == purchaseId))
                {
                    await _purchaseSqlDataService.Delete(purchaseId);
                    return true;
                }
            }
            return await _localStorage.DeletePurchase(purchaseId);
        }

        public async Task<List<PurchaseModel>> GetAll()
        {
            string userId = await _userUtilities.GetLoggedInUserId();
            if (userId is not null)
            {
                return await _purchaseSqlDataService.GetAll(userId);
            }
            else
            {
                return await _localStorage.GetPurchases();
            }
        }

        public async Task<int> GetItemsCountInCart()
        {
            string userId = await _userUtilities.GetLoggedInUserId();
            if (userId is not null)
            {
                return await _purchaseSqlDataService.GetItemsCountInCart(userId);
            }
            return await _localStorage.GetItemsCountInCart();
        }

        public async Task<bool> Update(int purchaseId, int quantity)
        {
            string userId = await _userUtilities.GetLoggedInUserId();
            if (userId is not null)
            {
                // We have to ensure that this particular purchase is indeed belong current user
                // Don't let client side ability to arbitrary update any purchase 
                List<PurchaseModel> purchases = await _purchaseSqlDataService.GetAll(userId);
                if (purchases.Exists(p => p.Id == purchaseId))
                {
                    await _purchaseSqlDataService.Update(purchaseId, quantity);
                    return true;
                }
            }
            return await _localStorage.UpdatePurchase(purchaseId, quantity);
        }
    }
}
