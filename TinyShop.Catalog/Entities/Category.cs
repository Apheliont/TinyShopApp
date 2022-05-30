using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using TinyShop.Catalog.DTOs;

namespace TinyShop.Catalog.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Image? Image { get; set; }
        public int? ParentId { get; set; }
        public Category? ParentNode { get; set; }
        public List<CategoryFilter>? CategoryFilters { get; set; }
        public List<Category>? SubCategories { get; set; }
        public List<Product>? Products { get; set; }
    }
}