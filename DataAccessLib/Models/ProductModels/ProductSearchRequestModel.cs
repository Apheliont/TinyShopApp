using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record ProductSearchRequestModel
    {
        public string SearchSentence { get; set; }
        public int NumberOfRecords { get; set; }
    }
}
