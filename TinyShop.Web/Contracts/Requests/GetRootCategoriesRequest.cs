using TinyShop.Web.DTOs;

namespace TinyShop.Contracts
{
    public record GetRootCategoriesRequest
    {
        public UserSettingsDto UserSettings { get; set; } = null!;
    }
}
