using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public enum OrderByEnum {
        [Description("By name")]
        ProductName = 1,
        [Description("By price")]
        Price = 2,
        [Description("By rating")]
        Rating = 3
    };

    public enum SortOrderEnum { 
        [Description("Reverse order")]
        DESC = 1,
        [Description("Straight order")]
        ASC = 2
    };

    public enum RowsPerPageEnum
    {
        [Description("10")]
        _10 = 10,
        [Description("25")]
        _25 = 25,
        [Description("50")]
        _50 = 50,
        [Description("80")]
        _80 = 80
    }

    #nullable enable
    public record ProductFilterModel
    {
        private int _pageNumber = 1;
        private decimal? _minPrice;
        private decimal? _maxPrice;

        public int? CategoryId { get; set; }
        public RowsPerPageEnum RowsPerPage { get; set; } = RowsPerPageEnum._25;
        public int PageNumber
        {
            get => _pageNumber; set
            {
                _pageNumber = value > 0 ? value : 1;
            }
        }
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.ProductName;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.DESC;
        public decimal? MinPrice
        {
            get => _minPrice; set
            {
                _minPrice = value is null ? null : value >= 0 ? value : 0;
            }
        }
        public decimal? MaxPrice
        {
            get => _maxPrice; set
            {
                _maxPrice = value is null ? null : value >= 0 ? value : 0;
            }
        }
    }
}
