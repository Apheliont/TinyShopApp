using DataAccessLib.Models;
using System.Collections.Generic;

namespace DataAccessLib.Data
{
    public interface IBreadcrumbSqlDataService
    {
        List<BreadcrumbItemModel> Get(int id, bool isProduct);
    }
}