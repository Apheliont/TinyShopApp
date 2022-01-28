using Nest;
using ProductSearchMicroservice.Models;

namespace ProductSearchMicroservice.DataAccess
{
    public class ElasticDataAccess : IElasticDataAccess
    {
        public IElasticClient ElasticClient { get; private set; }

        public ElasticDataAccess(string connectionString)
        {
            Uri uri = new Uri(connectionString);
            ConnectionSettings connectionSettings = new ConnectionSettings(uri)
                    .DefaultMappingFor<ElasticProductModel>(x => x.IndexName("products"));
            ElasticClient = new ElasticClient(connectionSettings);
        }
    }
}
