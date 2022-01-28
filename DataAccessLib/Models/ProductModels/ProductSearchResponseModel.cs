using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record ProductSearchResponseModel
    {
        public List<int> ProductIds { get; set; }
    }
}
