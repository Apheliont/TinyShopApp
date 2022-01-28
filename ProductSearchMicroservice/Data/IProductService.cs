using DataAccessLib.Models;

namespace ProductSearchMicroservice.Data
{
    public interface IProductService
    {
        Task<List<int>> SearchProducts(ProductSearchRequestModel requestModel);
    }
}
