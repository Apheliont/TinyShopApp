using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyShop.Catalog.Entities
{
    public class CategoryFilter
    {
        [Key]
        public int Id { get; set; }
        public int Index { get; set; } = 0;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public string? Measurement { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
