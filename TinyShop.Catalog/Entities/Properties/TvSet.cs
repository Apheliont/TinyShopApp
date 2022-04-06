using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Catalog.Entities
{
    public class TvSet
    {
        [Key]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public string Warranty { get; set; }
        public string MadeIn { get; set; }
        public int Voltage { get; set; }
        public bool HasSubwoofer { get; set; }
    }
}
