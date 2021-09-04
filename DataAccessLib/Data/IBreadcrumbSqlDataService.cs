using DataAccessLib.Models;

namespace DataAccessLib.Data
{
    public interface IBreadcrumbSqlDataService
    {
        List<BreadcrumbItemModel> Get(int id, bool isProduct);
    }
}