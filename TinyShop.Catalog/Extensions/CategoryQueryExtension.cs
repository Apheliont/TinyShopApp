using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Entities;

namespace TinyShop.Catalog.Extensions
{
    public static class CategoryQueryExtension
    {
        public static async Task<List<CategoryDto>> GetRoot(this AppDbContext db, string preferedLanguageCode, IMapper mapper)
        {
            List<Category> categories = await db.Categories
            .Include(c => c.Image)
            .Include(c => c.SubCategories)
            .Include(c => c.OriginalLanguage)
            .Where(c => c.ParentId == null)
            .ToListAsync();

            if (categories.Any(c => c.OriginalLanguage.LanguageCode
                != preferedLanguageCode))
            {
                IQueryable<CategoryTranslation> allTranslations = db.CategoryTranslations
                .Where(ct => ct.LanguageCode == preferedLanguageCode);

                categories = categories.GroupJoin(
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
            return categories.Select(c =>
            {
                var dto = mapper.Map<CategoryDto>(c);
                dto.IsParent = c.SubCategories.Any();
                return dto;
            }).ToList();
        }

        public static async Task<List<CategoryDto>> GetSubcategories(
            this AppDbContext db,
            string preferedLanguageCode,
            int categoryId,
            IMapper mapper)
        {
            List<Category> subcotegories = await db.Categories
                .Where(c => c.ParentId == categoryId)
                .Include(c => c.Image)
                .Include(c => c.SubCategories)
                .Include(c => c.OriginalLanguage)
                .ToListAsync();

            if (subcotegories.Any(sc => sc.OriginalLanguage.LanguageCode
                != preferedLanguageCode))
            {
                IQueryable<CategoryTranslation> allTranslations = db.CategoryTranslations
                .Where(ct => ct.LanguageCode == preferedLanguageCode);

                subcotegories = subcotegories.GroupJoin(
                    allTranslations,
                    sc => sc.Id,
                    ct => ct.CategoryId,
                    (sc, ct) => new { sc, ct })
                    .SelectMany(grp => grp.ct.DefaultIfEmpty(),
                    (grp, ct) =>
                    {
                        if (ct is null)
                        { return grp.sc; }
                        else
                        {
                            return mapper.Map(ct, grp.sc);
                        }
                    }).ToList();
            }

            return subcotegories.Select(sc =>
            {
                var dto = mapper.Map<CategoryDto>(sc);
                dto.IsParent = sc.SubCategories.Any();
                return dto;
            }).ToList();
        }
    }
}
