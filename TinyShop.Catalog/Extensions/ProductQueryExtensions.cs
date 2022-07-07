using TinyShop.Catalog.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using TinyShop.Catalog.DTOs;
using AutoMapper;
using Newtonsoft.Json;

namespace TinyShop.Catalog.Extensions
{
    public static class ProductQueryExtensions
    {
        public static async Task<ProductsInfoDto> FilterProducts(this AppDbContext db, ProductFilterDto productFilter, IMapper mapper)
        {
            ProductsInfoDto productsInfo = new();
            IQueryable<Product> allInCategoryQuery = db.Products
                .Include(p => p.Category).Include(p => p.OriginalLanguage)
                .Where(p => p.Category.Id == productFilter.CategoryId);
            IQueryable<Product> allWithDetailsQuery = allInCategoryQuery.Where(p => p.Details != null);
            IQueryable<Product> filteredProductsQuery = allInCategoryQuery;

            if (productFilter.Price is not null)
            {
                filteredProductsQuery = filteredProductsQuery.Where(p =>
                    p.Price >= productFilter.Price.LowerBound &&
                    p.Price <= productFilter.Price.UpperBound);
            }

            if (productFilter.CategoryFilters is not null)
            {
                foreach (CategoryFilterDto filter in productFilter.CategoryFilters)
                {
                    if (filter.Result is null) continue;

                    switch (filter.Type.ToLower())
                    {
                        case "range<double>":
                            {
                                RangeDto<double>? range = JsonConvert.DeserializeObject<RangeDto<double>>(filter.Result.ToString()!);
                                if (range is null) break;

                                filteredProductsQuery = filteredProductsQuery.Where(p =>
                                    p.Details == null ? false : p.Details
                                        .RootElement
                                        .GetProperty(filter.Name)
                                        .GetDouble() >= range.LowerBound &&
                                    p.Details
                                        .RootElement
                                        .GetProperty(filter.Name)
                                        .GetDouble() <= range.UpperBound);
                                break;
                            }

                        case "range<int>":
                            {
                                RangeDto<int>? range = JsonConvert.DeserializeObject<RangeDto<int>>(filter.Result.ToString()!);
                                if (range is null) break;

                                filteredProductsQuery = filteredProductsQuery.Where(p =>
                                    p.Details == null ? false : p.Details
                                        .RootElement
                                        .GetProperty(filter.Name)
                                        .GetInt32() >= range.LowerBound &&
                                    p.Details
                                        .RootElement
                                        .GetProperty(filter.Name)
                                        .GetInt32() <= range.UpperBound);
                                break;
                            }


                        case "checkbox":
                            {
                                List<string>? checkedItems = JsonConvert.DeserializeObject<List<string>>(filter.Result.ToString()!);
                                if (checkedItems == null || !checkedItems.Any()) break;

                                filteredProductsQuery = filteredProductsQuery.Where(p => p.Details == null ? false :
                                    checkedItems.Contains(
                                        p.Details
                                        .RootElement
                                        .GetProperty(filter.Name)
                                        .GetString() ?? ""));
                                break;
                            }

                        case "radio":
                            {
                                string checkedItem = filter.Result.ToString()!.ToLower();

                                filteredProductsQuery = filteredProductsQuery.Where(p =>
                                    p.Details == null ? false :
                                    p.Details.RootElement.GetProperty(filter.Name).GetString() == checkedItem);
                                break;
                            }

                    }
                }
            }


            productsInfo.Metadata.FoundRecords = filteredProductsQuery.Count();

            filteredProductsQuery = filteredProductsQuery.OrderBy($"{productFilter.OrderBy} {productFilter.SortOrder}");
            List<Product> products = await filteredProductsQuery.Skip((productFilter.PageNumber - 1) * productFilter.RowsPerPage)
                .Take(productFilter.RowsPerPage).ToListAsync();
            if (products.Count == 0) return productsInfo;

            string? preferedLanguageCode = productFilter.UserSettings?.PreferedLanguageCode;

            if (preferedLanguageCode is not null
                && products.Any(p => p.OriginalLanguage.LanguageCode
                != preferedLanguageCode))
            {
                IQueryable<ProductTranslation> allTranslations = db.ProductTranslations
                .Where(pt => pt.LanguageCode == preferedLanguageCode);

                products = products.GroupJoin(
                    allTranslations,
                    p => p.Id,
                    pt => pt.ProductId,
                    (p, pt) => new { p, pt })
                    .SelectMany(grp => grp.pt.DefaultIfEmpty(),
                    (grp, pt) =>
                    {
                        if (pt is null)
                        { return grp.p; }
                        else
                        {
                            return mapper.Map(pt, grp.p);
                        }
                    }).ToList();
            }


            productsInfo.Products = mapper.Map<List<ProductDto>>(products);

            return productsInfo;
        }

        public static async Task<ProductsInfoDto> GetProductsAndInfo(this AppDbContext db, ProductFilterDto productFilter, IMapper mapper)
        {
            ProductsInfoDto productsInfo = await FilterProducts(db, productFilter, mapper);

            IQueryable<Product> allInCategoryQuery = db
                .Products
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryFilters)
                .Include(p => p.Category)
                    .ThenInclude(c => c.OriginalLanguage)
                .Where(p => p.Category.Id == productFilter.CategoryId);
            IQueryable<Product> allWithDetailsQuery = allInCategoryQuery.Where(p => p.Details != null);
            IQueryable<Product> filteredProductsQuery = allInCategoryQuery;

            List<CategoryFilter>? categoryFilters = allInCategoryQuery.FirstOrDefault()?.Category.CategoryFilters;

            if (categoryFilters is not null)
            {
                string? preferedLanguageCode = productFilter.UserSettings?.PreferedLanguageCode;
                if (categoryFilters.Any(c => c.OriginalLanguage.LanguageCode
                != preferedLanguageCode))
                {
                    IQueryable<CategoryFilterTranslation> allTranslations = db.CategoryFilterTranslations
                    .Where(cft => cft.LanguageCode == preferedLanguageCode);

                    categoryFilters = categoryFilters.GroupJoin(
                        allTranslations,
                        cf => cf.Id,
                        cft => cft.CategoryFilterId,
                        (cf, cft) => new { cf, cft })
                        .SelectMany(grp => grp.cft.DefaultIfEmpty(),
                        (grp, cft) =>
                        {
                            if (cft is null)
                            { return grp.cf; }
                            else
                            {
                                return mapper.Map(cft, grp.cf);
                            }
                        }).ToList();
                }

                productsInfo.Metadata.CategoryFilters = new List<CategoryFilterDto>();
                foreach (CategoryFilter filter in categoryFilters)
                {
                    CategoryFilterDto categoryFilterDto = mapper.Map<CategoryFilterDto>(filter);
                    switch (filter.Type.ToLower())
                    {
                        case "checkbox" or "radio":
                            {
                                var result = new List<string>();
                                foreach (Product product in allWithDetailsQuery)
                                {
                                    var det = product.Details;
                                    string productDetailName = det!.RootElement.GetProperty(filter.Name).ToString().ToLower();
                                    result.Add(productDetailName);
                                }

                                categoryFilterDto.Result = result.Distinct().ToList();
                                break;
                            }

                        case "range<double>":
                            {
                                var result = allWithDetailsQuery
                                    .Select(t =>
                                         t.Details!
                                        .RootElement
                                        .GetProperty(filter.Name)
                                        .GetDouble())
                                    .ToList();
                                categoryFilterDto.Result = new RangeDto<double>
                                {
                                    LowerBound = result.Min(),
                                    UpperBound = result.Max()
                                };
                                break;
                            }

                        case "range<int>":
                            {
                                var result = allWithDetailsQuery
                                    .Select(t =>
                                         t.Details!
                                        .RootElement
                                        .GetProperty(filter.Name)
                                        .GetInt32())
                                    .ToList();

                                categoryFilterDto.Result = new RangeDto<int>
                                {
                                    LowerBound = result.Min(),
                                    UpperBound = result.Max()
                                };
                                break;
                            }

                    }
                    productsInfo.Metadata.CategoryFilters.Add(categoryFilterDto);
                }
            }

            productsInfo.Metadata.Price = await allInCategoryQuery
                .GroupBy(p => 1)
                .Select(p =>
                    new RangeDto<int>
                    {
                        LowerBound = (int)Math.Floor(p.Min(g => g.Price)),
                        UpperBound = (int)Math.Ceiling(p.Max(g => g.Price))
                    }).FirstOrDefaultAsync();

            return productsInfo;
        }
    }
}
