using DataAccessLib.Models.ElasticModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public interface IProductElasticService
    {
        Task<List<int>> SearchProducts(string searchSentence, int numberOfRecords);
    }
}
