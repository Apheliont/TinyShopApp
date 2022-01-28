using DataAccessLib.Models;
using Nest;
using ProductSearchMicroservice.DataAccess;
using ProductSearchMicroservice.Models;

namespace ProductSearchMicroservice.Data
{
    public class ProductService : IProductService
    {
        private readonly IElasticDataAccess _elasticDataAccess;
        public ProductService(IElasticDataAccess elasticDataAccess)
        {
            _elasticDataAccess = elasticDataAccess;
        }
        public async Task<List<int>> SearchProducts(ProductSearchRequestModel requestModel)
        {
            var res = await _elasticDataAccess.ElasticClient.SearchAsync<ElasticProductModel>(s => s
                                .Size(requestModel.NumberOfRecords)
                                .Query(q =>
                                        q.MultiMatch(c => c
                                        .Fields(f => f.Field(p => p.ProductName, 1.5).Field(p => p.Description))
                                        .Query(requestModel.SearchSentence)
                                        .Operator(Operator.Or)
                                        ))
                                    );
            return res.Hits.Select(x => x.Source.Id).ToList();
        }
    }
}
