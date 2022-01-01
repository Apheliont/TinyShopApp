using DataAccessLib.DataAccess;
using DataAccessLib.Models.ElasticModel;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Data
{
    public class ProductElasticService : IProductElasticService
    {
        private readonly IElasticDataAccess _elasticDataAccess;
        public ProductElasticService(IElasticDataAccess elasticDataAccess)
        {
            _elasticDataAccess = elasticDataAccess;
        }
        public async Task<List<int>> SearchProducts(string searchSentence, int numberOfRecords)
        {
            var res = await _elasticDataAccess.ElasticClient.SearchAsync<ProductModel>(s => s
                                .Size(numberOfRecords)
                                .Query(q =>
                                        q.MultiMatch(c => c
                                        .Fields(f => f.Field(p => p.ProductName, 1.5).Field(p => p.Description))
                                        .Query(searchSentence)
                                        .Operator(Operator.Or)
                                        ))
                                    );
            return res.Hits.Select(x => x.Source.Id).ToList();
        }
    }
}
