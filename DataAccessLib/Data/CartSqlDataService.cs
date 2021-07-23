using DataAccessLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public class CartSqlDataService : ICartSqlDataService
    {
        private readonly ISqlDataAccess _dataAccess;
        public CartSqlDataService(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task AddPurchase(string userId, int productId, int quantity)
        {
            await _dataAccess.SaveData<dynamic>("spCarts_AddPurchase", new
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity
            });
        }
    }
}
