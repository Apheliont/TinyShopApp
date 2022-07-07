using System.ComponentModel.DataAnnotations;

namespace TinyShop.Catalog.Entities
{
    public class Country
    {
        [Key]
        public string CountryCode { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string CountryName { get; set; } = null!;
        [Required]
        [MaxLength(4)]
        public string CurrencySign { get; set; } = null!;

    }
}
