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
        private ConcurrentDictionary<int, List<CategoryParentModel>> cache = new ConcurrentDictionary<int, List<CategoryParentModel>>();
        private readonly ICategorySqlDataService _categoryDataService;

        public UriUtils(ICategorySqlDataService categoryDataService)
        {
            _categoryDataService = categoryDataService;
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

        public async Task<List<CategoryParentModel>> GetParents(string uri)
        {
            if (GetIdFromURI(uri, "categories", out int categoryId))
            {
                if (cache.ContainsKey(categoryId))
                {
                    return cache.GetValueOrDefault(categoryId);
                }
                else
                {
                    var res = await _categoryDataService.GetParents(categoryId, false);
                    if (res.Any())
                    {
                        cache.TryAdd(categoryId, res);
                        return res;
                    }
                    else
                    {
                        cache.TryAdd(categoryId, null);
                        return null;
                    }
                }
            }

            if (GetIdFromURI(uri, "products", out int productId))
            {
                if (cache.ContainsKey(productId))
                {
                    return cache.GetValueOrDefault(productId);
                }
                else
                {
                    var res = await _categoryDataService.GetParents(productId, true);
                    if (res.Any())
                    {
                        cache.TryAdd(productId, res);
                        return res;
                    }
                    else
                    {
                        cache.TryAdd(productId, null);
                        return null;
                    }
                }
            }

            return null;
        }

    }
}
