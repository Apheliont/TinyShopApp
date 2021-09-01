using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record DetailedProductModel
    {
        public ProductModel ProductInfo { get; set; }
        public Dictionary<string,dynamic> ProductDetails { get; set; }
    }
}
