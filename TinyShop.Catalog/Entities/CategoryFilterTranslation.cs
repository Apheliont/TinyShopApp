using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyShop.Catalog.Entities
{
    public class CategoryFilterTranslation
    {
        [ForeignKey(nameof(CategoryFilter))]
        public int CategoryFilterId { get; set; }

        [ForeignKey(nameof(Language))]
        public string LanguageCode { get; set; } = null!;

        public string? Description { get; set; }
        public string? Measurement { get; set; }
    }
}
