namespace TinyShop.Web.Models
{
    public class ImageModel
    {
        public string Caption { get; set; }
        public string UriSizeS { get; set; }
        public string UriSizeM { get; set; } = null!;
        public string UriSizeL { get; set; }
        public bool IsMain { get; set; } = false;
    }
}