using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface ICartSqlDataService
    {
        Task AddPurchase(string userId, int productId, int quantity);
    }
}