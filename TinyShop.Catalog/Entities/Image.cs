using System.ComponentModel.DataAnnotations;

namespace TinyShop.Catalog.Entities
{
    public class Image
    {
        [Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(300)]
		public string Caption { get; set; } = "Image";
        [MaxLength(500)]
		public string? UriSizeS { get; set; }
		[Required]
		[MaxLength(500)]
		public string UriSizeM { get; set; } = null!;
		[MaxLength(500)]
		public string? UriSizeL { get; set; }
		public bool IsMain { get; set; } = false;
		public List<Product> Products { get; set; } = new();
		public List<ProductsImages> ProductsImages { get; set; } = new();

	}
}