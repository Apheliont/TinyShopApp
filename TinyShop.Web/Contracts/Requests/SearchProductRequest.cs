namespace TinyShop.Contracts
{
    public record SearchProductRequest
    {
        public string SearchSentence { get; init; }
        public int NumberOfRecords { get; init; }
    }
}
