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
        private int _rowsPerPage = 1;
        private int _pageNumber = 1;
        private decimal? _minPrice;
        private decimal? _maxPrice;

        public int? CategoryId { get; set; }
        public int RowsPerPage
        {
            get => _rowsPerPage; set
            {
                _rowsPerPage = value > 0 ? value : 1;
            }
        }
        public int PageNumber
        {
            get => _pageNumber; set
            {
                _pageNumber = value > 0 ? value : 1;
            }
        }
        public string OrderBy { get; set; } = "ProductName";
        public string OrderType { get; set; } = "DESC";
        public decimal? MinPrice
        {
            get => _minPrice; set
            {
                _minPrice = value >= 0 ? value : 0;
            }
        }
        public decimal? MaxPrice
        {
            get => _maxPrice; set
            {
                _maxPrice = value >= 0 ? value : 0;
            }
        }

        public void ResetModel()
        {
            CategoryId = null;
            RowsPerPage = 20;
            PageNumber = 1;
            OrderBy = "ProductName";
            OrderType = "DESC";
            MinPrice = null;
            MaxPrice = null;
        }
    }
}
