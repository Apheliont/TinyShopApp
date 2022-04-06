using System.Collections.Generic;
using TinyShop.Web.DTOs;

namespace TinyShop.Contracts
{
    public record GetRootCategoriesResponse
    {
        public List<CategoryDto> Categories { get; set; }
    }
}
