using AutoMapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Web.DTOs;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public class UriService : IUriService
    {
        private ConcurrentDictionary<string, List<BreadcrumbModel>> categoryCache = new ConcurrentDictionary<string, List<BreadcrumbModel>>();
        private ConcurrentDictionary<string, List<BreadcrumbModel>> productCache = new ConcurrentDictionary<string, List<BreadcrumbModel>>();
        private readonly IBreadcrumbsService _breadcrumbsService;
        private readonly IMapper _mapper;

        public UriService(IBreadcrumbsService breadcrumbsService, IMapper mapper)
        {
            _breadcrumbsService = breadcrumbsService;
            _mapper = mapper;
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

        public async Task<List<BreadcrumbModel>> GetBreadcrumbs(string uri, UserSettings userSettings)
        {
            UserSettingsDto userSettingsDto = _mapper.Map<UserSettingsDto>(userSettings);

            if (GetIdFromURI(uri, "categories", out int categoryId))
            {
                var key = $"{categoryId}-{userSettings.PreferedLanguageCode}";
                if (categoryCache.ContainsKey(key))
                {
                    return categoryCache.GetValueOrDefault(key)!;
                }
                else
                {
                    var res = await _breadcrumbsService.Get(categoryId, false, userSettingsDto);
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

                        categoryCache.TryAdd(key, res);
                        return res;
                    }
                }
            }

            if (GetIdFromURI(uri, "products", out int productId))
            {
                var key = $"{productId}-{userSettings.PreferedLanguageCode}";
                if (productCache.ContainsKey(key))
                {
                    return productCache.GetValueOrDefault(key)!;
                }
                else
                {
                    var res = await _breadcrumbsService.Get(productId, true, userSettingsDto);
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

                        productCache.TryAdd(key, res);
                        return res;
                    }
                }
            }
            return new List<BreadcrumbModel>();
        }

    }
}