namespace TinyShop.Contracts
{
    public record SearchProductRequest
    {
        public string SearchSentence { get; init; } = null!;
        public int NumberOfRecords { get; init; }
    }
}
