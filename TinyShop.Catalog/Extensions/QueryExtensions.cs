using TinyShop.Catalog.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using TinyShop.Catalog.DTOs;
using AutoMapper;
using Newtonsoft.Json;

namespace TinyShop.Catalog.Extensions
{
    public static class QueryExtensions
    {
        public static async Task<ProductsInfoDto> FilterProducts(this IQueryable<Product> query, ProductFilterDto productFilter, IMapper mapper)
        {
            ProductsInfoDto productsInfo = new();
            IQueryable<Product> allInCategoryQuery = query.Where(p => p.Category.Id == productFilter.CategoryId);
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

            productsInfo.Products = mapper.Map<List<ProductDto>>(products);

            return productsInfo;
        }

        public static async Task<ProductsInfoDto> GetProductsAndInfo(this IQueryable<Product> query, ProductFilterDto productFilter, IMapper mapper)
        {
            ProductsInfoDto productsInfo = await FilterProducts(query, productFilter, mapper);



            IQueryable<Product> allInCategoryQuery = query.Where(p => p.Category.Id == productFilter.CategoryId);
            IQueryable<Product> allWithDetailsQuery = allInCategoryQuery.Where(p => p.Details != null);
            IQueryable<Product> filteredProductsQuery = allInCategoryQuery;

            List<CategoryFilter>? categoryFilters = allInCategoryQuery.FirstOrDefault()?.Category.CategoryFilters;

            if (categoryFilters is not null)
            {
                productsInfo.Metadata.CategoryFilters = new List<CategoryFilterDto>();
                foreach (CategoryFilter filter in categoryFilters)
                {
                    CategoryFilterDto categoryFilterDto = mapper.Map<CategoryFilterDto>(filter);
                    switch (filter.Type.ToLower())
                    {
                        case "checkbox" or "radio":
                            {
                                var result = new List<string>();
                                foreach(Product product in allWithDetailsQuery)
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
