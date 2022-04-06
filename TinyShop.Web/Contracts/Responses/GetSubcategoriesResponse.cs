using System.Collections.Generic;
using TinyShop.Web.DTOs;

namespace TinyShop.Contracts
{
    public record GetSubcategoriesResponse
    {
        public List<CategoryDto> Subcategories { get; set; }
    }
}
