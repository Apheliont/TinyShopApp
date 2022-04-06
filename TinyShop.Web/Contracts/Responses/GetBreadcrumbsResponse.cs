using System.Collections.Generic;
using TinyShop.Web.DTOs;

namespace TinyShop.Contracts
{
    public record GetBreadcrumbsResponse
    {
        public List<BreadcrumbDto> Breadcrumbs { get; set; }
    }
}
