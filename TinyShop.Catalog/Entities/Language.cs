using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyShop.Catalog.Entities
{
    public class Language
    {
        [Key]
        public string LanguageCode { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string LanguageName { get; set; } = null!;
        public List<Country>? Countries { get; set; }
    }
}
