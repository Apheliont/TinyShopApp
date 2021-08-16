using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record ImageModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string UriSizeS { get; set; }
        public string UriSizeM { get; set; }
        public string UriSizeL { get; set; }
        public bool IsMain { get; set; }
    }
}
