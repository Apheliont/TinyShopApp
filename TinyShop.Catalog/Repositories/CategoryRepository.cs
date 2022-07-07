using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Entities;
using TinyShop.Catalog.Extensions;

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

        public async Task<List<CategoryDto>> GetRoot(UserSettingsDto userSettings)
        {
            return await _db.GetRoot(userSettings.PreferedLanguageCode, _mapper);
        }

        public async Task<List<CategoryDto>> GetSubcategories(int categoryId, UserSettingsDto userSettings)
        {
            return await _db.GetSubcategories(userSettings.PreferedLanguageCode, categoryId, _mapper);
        }
    }
}
