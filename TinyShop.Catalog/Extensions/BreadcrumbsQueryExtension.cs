
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Entities;
using TinyShop.Contracts;

namespace TinyShop.Catalog.Extensions
{
    public static class BreadcrumbsQueryExtension
    {
        public static async Task<List<BreadcrumbDto>> GetBreadcrumbs(this AppDbContext db, GetBreadcrumbsRequest request, IMapper mapper)
        {
            int id = request.Id;
            bool isProduct = request.IsProduct;
            string preferedLanguageCode = request.UserSettings.PreferedLanguageCode;

            List<BreadcrumbDto> result = new List<BreadcrumbDto>();
            int currentId = id;
            if (request.IsProduct)
            {
                Product? foundProduct = await db.Products.Include(product => product.Category)
                                                .Include(p => p.OriginalLanguage)
                                                .SingleOrDefaultAsync(p => p.Id == id);
                if (foundProduct is null)
                {
                    return result;
                }

                if (foundProduct.OriginalLanguage.LanguageCode != preferedLanguageCode)
                {
                    ProductTranslation? productTranslation = await db.ProductTranslations
                                .Where(pt => pt.LanguageCode == preferedLanguageCode && pt.ProductId == id).SingleOrDefaultAsync();

                    mapper.Map(productTranslation, foundProduct);
                }

                result.Add(mapper.Map<BreadcrumbDto>(foundProduct));
                currentId = foundProduct.Category.Id;
            }
            List<Category> allCategories = await db.Categories.Include(c => c.OriginalLanguage).ToListAsync();
            if (allCategories.Any(c => c.OriginalLanguage.LanguageCode
                != preferedLanguageCode))
            {
                IQueryable<CategoryTranslation> allTranslations = db.CategoryTranslations
                .Where(pt => pt.LanguageCode == preferedLanguageCode);

                allCategories = allCategories.GroupJoin(
                    allTranslations,
                    c => c.Id,
                    ct => ct.CategoryId,
                    (c, ct) => new { c, ct })
                    .SelectMany(grp => grp.ct.DefaultIfEmpty(),
                    (grp, ct) =>
                    {
                        if (ct is null)
                        { return grp.c; }
                        else
                        {
                            return mapper.Map(ct, grp.c);
                        }
                    }).ToList();
            }


            while (true)
            {
                Category? foundCategory = allCategories.SingleOrDefault(c => c.Id == currentId);
                if (foundCategory is not null)
                {
                    result.Add(mapper.Map<BreadcrumbDto>(foundCategory));
                    if (foundCategory.ParentId is null)
                    {
                        break;
                    }

                    currentId = foundCategory.ParentId.Value;
                }
            }
            result.Reverse();
            return result;
        }
    }
}
