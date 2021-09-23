using DataAccessLib.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{

#nullable enable
    public record ProductFilterModel
    {
        public RatingType Rating { get; set; } = new RatingType { UpperBound = 5 };
        public int CategoryId { get; set; }
        public RowsPerPageEnum RowsPerPage { get; set; } = RowsPerPageEnum._25;
        public int PageNumber {  get; set; } = 1;
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ProductName;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.DESC;
        public RangeType Price { get; set; } = new RangeType();

        // Optional field
        public DetailsFilterModel? DetailsFilterModel {  get; set; }
    }
}
