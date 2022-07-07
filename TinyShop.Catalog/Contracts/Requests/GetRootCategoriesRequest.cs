using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record GetRootCategoriesRequest
    {
        public UserSettingsDto UserSettings { get; set; } = null!;
    }
}
