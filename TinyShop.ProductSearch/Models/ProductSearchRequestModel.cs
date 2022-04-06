namespace TinyShop.ProductSearch.Models
{
    public record ProductSearchRequestModel
    {
        public string SearchSentence { get; set; }
        public int NumberOfRecords { get; set; }
    }
}
