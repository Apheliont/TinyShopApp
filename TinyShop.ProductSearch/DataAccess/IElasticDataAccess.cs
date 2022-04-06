using Nest;

namespace TinyShop.ProductSearch.DataAccess
{
    public interface IElasticDataAccess
    {
        IElasticClient ElasticClient { get; }
    }
}
