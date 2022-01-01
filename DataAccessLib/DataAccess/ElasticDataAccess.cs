using DataAccessLib.Models.ElasticModel;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.DataAccess
{
    public class ElasticDataAccess : IElasticDataAccess
    {
        public IElasticClient ElasticClient { get; private set; }

        public ElasticDataAccess(string connectionString)
        {
            Uri uri = new Uri(connectionString);
            ConnectionSettings connectionSettings = new ConnectionSettings(uri)
                    .DefaultMappingFor<ProductModel>(x => x.IndexName("products"));
            ElasticClient = new ElasticClient(connectionSettings);
        }
    }
}
