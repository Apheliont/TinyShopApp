using AutoMapper;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyShop.Web.DTOs;
using TinyShop.Web.Models;
using TinyShop.Contracts;

namespace TinyShop.Web.Services
{
    public class BreadcrumbsService : IBreadcrumbsService
    {
        private readonly IRequestClient<GetBreadcrumbsRequest> _requestClient;
        private readonly IMapper _mapper;
        public BreadcrumbsService(IRequestClient<GetBreadcrumbsRequest> requestClient, IMapper mapper)
        {
            _requestClient = requestClient;
            _mapper = mapper;
        }
        public async Task<List<BreadcrumbModel>> Get(int id, bool isProduct, UserSettingsDto userSettings)
        {
            var result = await _requestClient.GetResponse<GetBreadcrumbsResponse>(new
            GetBreadcrumbsRequest
            {
                Id = id,
                IsProduct = isProduct,
                UserSettings = userSettings
            });
            return _mapper.Map<List<BreadcrumbModel>>(result.Message.Breadcrumbs);
        }
    }
}
