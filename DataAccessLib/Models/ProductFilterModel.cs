using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    #nullable enable
    public class ProductFilterModel
    {
        public int? CategoryId { get; set; }
        public int RowsPerPage { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
        public string? OrderBy { get; set; }
        public string? OrderType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
