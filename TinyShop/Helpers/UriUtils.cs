using DataAccessLib.Data;
using DataAccessLib.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Helpers
{
    public class UriUtils : IUriUtils
    {
        private ConcurrentDictionary<int, List<BreadcrumbItemModel>> categoryCache = new ConcurrentDictionary<int, List<BreadcrumbItemModel>>();
        private ConcurrentDictionary<int, List<BreadcrumbItemModel>> productCache = new ConcurrentDictionary<int, List<BreadcrumbItemModel>>();
        private readonly IBreadcrumbSqlDataService _breadcrumbDataService;

        public UriUtils(IBreadcrumbSqlDataService breadcrumbDataService)
        {
            _breadcrumbDataService = breadcrumbDataService;
        }

        private bool GetIdFromURI(string uri, string type, out int id)
        {
            id = 0;
            if (uri.Contains(type))
            {
                var a = uri.Split('/').AsEnumerable();
                return Int32.TryParse(a.SkipWhile(t => t != type).ElementAtOrDefault(1) ?? "", out id);
            }
            return false;
        }

        public List<BreadcrumbItemModel> GetBreadcrumbs(string uri)
        {
            if (GetIdFromURI(uri, "categories", out int categoryId))
            {
                if (categoryCache.ContainsKey(categoryId))
                {
                    return categoryCache.GetValueOrDefault(categoryId);
                }
                else
                {
                    var res = _breadcrumbDataService.Get(categoryId, false);
                    if (res.Any())
                    {
                        int lastIdx = res.Count - 1;
                        res = res.Select((item, idx) =>
                        {
                            if (idx != lastIdx)
                            {
                                item.Uri = $"/categories/{item.Id}";
                            }

                            return item;
                        }).ToList();

                        categoryCache.TryAdd(categoryId, res);
                        return res;
                    }
                    else
                    {
                        categoryCache.TryAdd(categoryId, null);
                        return null;
                    }
                }
            }

            if (GetIdFromURI(uri, "products", out int productId))
            {
                if (productCache.ContainsKey(productId))
                {
                    return productCache.GetValueOrDefault(productId);
                }
                else
                {
                    var res = _breadcrumbDataService.Get(productId, true);
                    if (res.Any())
                    {
                        int lastIdx = res.Count - 1;
                        res = res.Select((item, idx) =>
                        {
                            if (idx == lastIdx - 1)
                            {
                                item.Uri = $"/categories/{item.Id}/products";
                            }
                            else if (idx != lastIdx)
                            {
                                item.Uri = $"/categories/{item.Id}";
                            }

                            return item;
                        }).ToList();

                        productCache.TryAdd(productId, res);
                        return res;
                    }
                    else
                    {
                        productCache.TryAdd(productId, null);
                        return null;
                    }
                }
            }

            return null;
        }

    }
}
