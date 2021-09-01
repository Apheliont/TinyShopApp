using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record CategoryParentModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
