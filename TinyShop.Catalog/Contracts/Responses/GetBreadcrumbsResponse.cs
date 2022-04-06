using TinyShop.Catalog.DTOs;

namespace TinyShop.Contracts
{
    public record GetBreadcrumbsResponse
    {
        public List<BreadcrumbDto> Breadcrumbs { get; set; }
    }
}
