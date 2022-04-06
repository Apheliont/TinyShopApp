namespace TinyShop.Contracts
{
    public record SearchProductContract
    {
        public string SearchSentence { get; init; }
        public int NumberOfRecords { get; init; }
    }
}
