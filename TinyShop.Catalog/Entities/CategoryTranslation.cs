using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Catalog.Entities
{
    public class CategoryTranslation
    {
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(Language))]
        public string LanguageCode { get; set; } = null!;

        [MaxLength(200)]
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}