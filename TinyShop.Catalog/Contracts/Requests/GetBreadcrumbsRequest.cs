using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record GetBreadcrumbsRequest
    {
        public int Id { get; init; }
        public bool IsProduct { get; init; }
        public UserSettingsDto UserSettings { get; set; } = null!;
    }
}
