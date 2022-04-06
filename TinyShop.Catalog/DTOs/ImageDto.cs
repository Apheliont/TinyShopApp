namespace TinyShop.Catalog.DTOs
{
    public class ImageDto
    {
		public string Caption { get; set; }
		public string UriSizeS { get; set; }
		public string UriSizeM { get; set; } = null!;
		public string UriSizeL { get; set; }
		public bool IsMain { get; set; } = false;
	}
}