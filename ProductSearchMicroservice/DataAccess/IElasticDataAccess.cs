using Nest;

namespace ProductSearchMicroservice.DataAccess
{
    public interface IElasticDataAccess
    {
        IElasticClient ElasticClient { get; }
    }
}
