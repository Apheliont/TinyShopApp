using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public class BreadcrumbSqlDataService : IBreadcrumbSqlDataService
    {
        private readonly ISqlDataAccess _dataAccess;
        public BreadcrumbSqlDataService(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<BreadcrumbItemModel> Get(int id, bool isProduct)
        {
            string jsonText = _dataAccess
                        .GetJsonText<dynamic>("spBreadcrumbs_Get", new { Id = id, IsProduct = isProduct });
            return JsonConvert.DeserializeObject<List<BreadcrumbItemModel>>(jsonText);
        }
    }
}
