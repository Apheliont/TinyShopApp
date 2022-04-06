using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Entities;

namespace TinyShop.Catalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public CategoryRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetRoot()
        {
            List<Category> categories = await _db.Categories
                .Include(c => c.Image)
                .Include(c => c.SubCategories)
                .Where(c => c.ParentId == null)
                .ToListAsync();
            return categories.Select(c =>
                {
                    var dto = _mapper.Map<CategoryDto>(c);
                    dto.IsParent = c.SubCategories.Any();
                    return dto;
                }).ToList();
        }

        public async Task<List<CategoryDto>> GetSubcategories(int categoryId)
        {
            List<Category> subcotegories = await _db.Categories
                .Where(c => c.ParentId == categoryId)
                .Include(c => c.Image)
                .Include(c => c.SubCategories)
                .ToListAsync();
            return subcotegories.Select(sc =>
            {
                var dto = _mapper.Map<CategoryDto>(sc);
                dto.IsParent = sc.SubCategories.Any();
                return dto;
            }).ToList();
        }
    }
}
