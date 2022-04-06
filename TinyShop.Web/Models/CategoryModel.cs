namespace TinyShop.Web.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ImageModel Image { get; set; }
        public bool IsParent { get; set; }
    }
}
