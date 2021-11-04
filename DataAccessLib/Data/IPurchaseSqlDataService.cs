using DataAccessLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IPurchaseSqlDataService
    {
        Task AddToCart(string userId, int productId, int quantity);
        Task Delete(int purchaseId);
        Task<List<PurchaseModel>> GetAll(string userId);
        Task<int> GetItemsCountInCart(string userId);
        Task Update(int purchaseId, int quantity);
    }
}