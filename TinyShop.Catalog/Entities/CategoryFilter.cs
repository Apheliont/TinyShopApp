using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Catalog.Entities
{
    public class CategoryFilter
    {
        [Key]
        public int Id { get; set; }
        public Language OriginalLanguage { get; set; } = null!;
        public int Index { get; set; } = 0;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public string? Measurement { get; set; }
        public List<Category>? Categories { get; set; }
        public List<CategoryFilterTranslation>? CategoryFilterTranslations { get; set; }
    }
}
