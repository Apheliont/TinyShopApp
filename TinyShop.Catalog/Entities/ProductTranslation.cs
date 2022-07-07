using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Catalog.Entities
{
    public class ProductTranslation
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [ForeignKey(nameof(Language))]
        public string LanguageCode { get; set; } = null!;

        [MaxLength(200)]
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        
    }
}
