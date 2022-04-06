using AutoMapper;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Contracts;
using TinyShop.Web.DTOs;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRequestClient<GetRootCategoriesRequest> _getCategoryRoot;
        private readonly IRequestClient<GetSubcategoriesRequest> _getSubcategories;
        private readonly IMapper _mapper;
        public CategoryService
            (
                IRequestClient<GetRootCategoriesRequest> getCategoryRoot,
                IRequestClient<GetSubcategoriesRequest> getSubcategories,
                IMapper mapper
            )
        {
            _getCategoryRoot = getCategoryRoot;
            _getSubcategories = getSubcategories;
            _mapper = mapper;
        }
        public async Task<List<CategoryModel>> GetRoot()
        {
            var res = await _getCategoryRoot.GetResponse<GetRootCategoriesResponse>(new { });
            return _mapper.Map<List<CategoryModel>>(res.Message.Categories);
        }

        public async Task<List<CategoryModel>> GetSubcategories(int categoryId)
        {
            var res = await _getSubcategories
                .GetResponse<GetSubcategoriesResponse>
                    (new GetSubcategoriesRequest { CategoryId = categoryId });
            return _mapper.Map<List<CategoryModel>>(res.Message.Subcategories);
        }
    }
}
