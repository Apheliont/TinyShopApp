using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.DataAccess
{
    public interface IElasticDataAccess
    {
        IElasticClient ElasticClient { get; }
    }
}
