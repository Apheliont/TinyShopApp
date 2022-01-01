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
        public async Task<int> AddToCart(PurchaseModel purchase)
        {
            string userId = await _userUtilities.GetLoggedInUserId();
            if (userId is not null)
            {
                return await _purchaseSqlDataService.AddToCart(userId, purchase.ProductId);
            }
            else
            {
                // as we use localStorage we must set purchase.Id to some value
                purchase.Id = purchase.ProductId;
                await _localStorage.AddToCart(purchase);
                return purchase.ProductId;
            }
        }

        public async Task<bool> Delete(int purchaseId)
        {
            string userId = await _userUtilities.GetLoggedInUserId();
            if (userId is not null)
            {
                // We have to ensure that this particular purchase is indeed belong to the current user
                // Don't let client side ability to arbitrary delete any purchases 
                List<PurchaseModel> purchases = await _purchaseSqlDataService.GetAll(userId);
                if (purchases.Exists(p => p.Id == purchaseId))
                {
                    await _purchaseSqlDataService.Delete(userId, purchaseId);
                    return true;
                }
                return false;
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
                // We have to ensure that this particular purchase is indeed belong to the current user
                // Don't let client side ability to arbitrary delete any purchases 
                List<PurchaseModel> purchases = await _purchaseSqlDataService.GetAll(userId);
                if (purchases.Exists(p => p.Id == purchaseId))
                {
                    await _purchaseSqlDataService.Update(userId, purchaseId, quantity);
                    return true;
                }
                return false;
            }
            return await _localStorage.UpdatePurchase(purchaseId, quantity);
        }
    }
}
