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
        public int CategoryId { get; set; }
        public RowsPerPageEnum RowsPerPage { get; set; } = RowsPerPageEnum._25;
        public int PageNumber { get; set; } = 1;
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ProductName;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.DESC;

        // next fields will be packed and sent back as json as they are not strictly necessary to be present
        public RatingType Rating { get; set; } = new RatingType { UpperBound = 5 };
        public RangeType Price { get; set; } = new RangeType();

        // Optional field
        public DetailsFilterModel? DetailsFilterModel {  get; set; }
    }
}
