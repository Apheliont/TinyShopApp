using TinyShop.Web.DTOs;

namespace TinyShop.Contracts
{
    public record GetSubcategoriesRequest
    {
        public int CategoryId { get; init; }
        public UserSettingsDto UserSettings { get; set; } = null!;
    }
}
