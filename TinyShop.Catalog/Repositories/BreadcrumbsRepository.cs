using Microsoft.EntityFrameworkCore;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Entities;

namespace TinyShop.Catalog.Repositories
{
    public class BreadcrumbsRepository : IBreadcrumbsRepository
    {
        private readonly AppDbContext _db;
        public BreadcrumbsRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<BreadcrumbDto>> Get(int id, bool isProduct)
        {
            List<BreadcrumbDto> result = new List<BreadcrumbDto>();
            int currentId = id;
            if (isProduct)
            {
                Product? product = await _db.Products.Include(product => product.Categories)
                                                .SingleOrDefaultAsync(p => p.Id == id);
                if (product is not null)
                {
                    // TODO: Rework this logic to more precise
                    result.Add(new BreadcrumbDto { Id = product.Id, ItemName = product.ProductName, IsProduct = true });
                    var firstCategory = product.Categories.FirstOrDefault();
                    if (firstCategory is not null)
                    {
                        currentId = firstCategory.Id;
                    }
                }
            }
            List<Category> allCategories = await _db.Categories.ToListAsync();
            while (true)
            {
                var foundCategory = allCategories.SingleOrDefault(c => c.Id == currentId);
                if (foundCategory is not null)
                {
                    result.Add(new BreadcrumbDto { Id = currentId, ItemName = foundCategory.CategoryName });
                    if (foundCategory.ParentId is null)
                    {
                        break;
                    }

                    currentId = foundCategory.ParentId.Value;
                }
            }
            return result;
        }
    }
}
