using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public class PurchaseSqlDataService : IPurchaseSqlDataService
    {
        private readonly ISqlDataAccess _dataAccess;
        public PurchaseSqlDataService(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task AddToCart(string userId, int productId, int quantity)
        {
            await _dataAccess.SaveData<dynamic>("spPurchases_Add", new
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity > 0 ? quantity : 1
            });
        }

        public async Task<List<PurchaseModel>> GetAll(string userId)
        {
            return await _dataAccess.GetData<PurchaseModel, dynamic>("spPurchases_GetAllCartItems", new
            {
                UserId = userId
            });
        }

        public async Task Delete(int purchaseId)
        {
            await _dataAccess.SaveData<dynamic>("spPurchases_Delete", new { PurchaseId = purchaseId });
        }

        public async Task Update(int purchaseId, int quantity)
        {
            await _dataAccess.SaveData<dynamic>("spPurchases_Update",
            new
            {
                PurchaseId = purchaseId,
                Quantity = quantity > 0 ? quantity : 1
            });
        }
    }
}
