using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public class UriService : IUriService
    {
        private ConcurrentDictionary<int, List<BreadcrumbModel>> categoryCache = new ConcurrentDictionary<int, List<BreadcrumbModel>>();
        private ConcurrentDictionary<int, List<BreadcrumbModel>> productCache = new ConcurrentDictionary<int, List<BreadcrumbModel>>();
        private readonly IBreadcrumbsService _breadcrumbsService;

        public UriService(IBreadcrumbsService breadcrumbsService)
        {
            _breadcrumbsService = breadcrumbsService;
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

        public async Task<List<BreadcrumbModel>> GetBreadcrumbs(string uri)
        {
            if (GetIdFromURI(uri, "categories", out int categoryId))
            {
                if (categoryCache.ContainsKey(categoryId))
                {
                    return categoryCache.GetValueOrDefault(categoryId);
                }
                else
                {
                    var res = await _breadcrumbsService.Get(categoryId, false);
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
                    var res = await _breadcrumbsService.Get(productId, true);
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