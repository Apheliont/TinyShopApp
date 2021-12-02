using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IPurchaseSqlDataService
    {
        Task AddToCart(string userId, int productId, int quantity);
        Task Delete(string userId, int purchaseId);
        Task<List<PurchaseModel>> GetAll(string userId);
        Task<int> GetItemsCountInCart(string userId);
        Task Update(string userId, int purchaseId, int quantity);
    }
}