using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TinyShop.Catalog.Entities
{
    public class Product : IDisposable
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
        public Category Category { get; set; } = null!;
        public List<ProductsImages> ProductsImages { get; set; } = new();
        [Column(TypeName = "jsonb")]
        public JsonDocument? Details { get; set; }
        public void Dispose() => Details?.Dispose();
    }
}
