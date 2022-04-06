using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyShop.Catalog.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Image> Images { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<CategoriesProducts> CategoriesProducts { get; set; } = new(); 
        public List<ProductsImages> ProductsImages { get; set; } = new(); 
    }
}
