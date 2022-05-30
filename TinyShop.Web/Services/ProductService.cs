using AutoMapper;
using MassTransit;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using TinyShop.Contracts;
using TinyShop.Web.DTOs;
using TinyShop.Web.Models;

namespace TinyShop.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRequestClient<FilterProductsRequest> _filterProduct;
        private readonly IRequestClient<GetProductRequest> _getProduct;
        private readonly IRequestClient<GetProductsRequest> _getProducts;
        private readonly IRequestClient<GetProductsAndInfoRequest> _getProductsAndInfo;
        private readonly IRequestClient<SearchProductRequest> _searchProducts;
        public ProductService(
                IMapper mapper,
                IRequestClient<FilterProductsRequest> filterProduct,
                IRequestClient<GetProductRequest> getProduct,
                IRequestClient<GetProductsRequest> getProducts,
                IRequestClient<GetProductsAndInfoRequest> getProductsAndInfo,
                IRequestClient<SearchProductRequest> searchProducts
            )
        {
            _mapper = mapper;
            _filterProduct = filterProduct;
            _getProduct = getProduct;
            _getProducts = getProducts;
            _getProductsAndInfo = getProductsAndInfo;
            _searchProducts = searchProducts;
        }
        public async Task<ProductsInfoModel> FilterProducts(ProductFilterModel filter)
        {
            var res = await _filterProduct.GetResponse<FilterProductsResponse>
                        (new FilterProductsRequest { Filter = _mapper.Map<ProductFilterDto>(filter) });
            return _mapper.Map<ProductsInfoModel>(res.Message.ProductsInfo);
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            var res = await _getProduct.GetResponse<GetProductResponse>
                        (new GetProductRequest { Id = id });
            return _mapper.Map<ProductModel>(res.Message.Product);
        }

        public async Task<List<ProductModel>> GetProducts(int[] ids)
        {
            var res = await _getProducts.GetResponse<GetProductsResponse>
                        (new GetProductsRequest { Ids = ids });
            return _mapper.Map<List<ProductModel>>(res.Message.Products);
        }

        public async Task<ProductsInfoModel> GetProductsAndInfo(ProductFilterModel filter)
        {
            var res = await _getProductsAndInfo.GetResponse<GetProductsAndInfoResponse>
            (new FilterProductsRequest { Filter = _mapper.Map<ProductFilterDto>(filter) });
            return _mapper.Map<ProductsInfoModel>(res.Message.ProductsInfo);
        }

        public async Task<List<ProductModel>> SearchProducts(string searchSentence, int numberOfRecords, CancellationToken token)
        {
            var response = await _searchProducts.GetResponse<SearchProductsResponse>(
                new SearchProductRequest { SearchSentence = searchSentence, NumberOfRecords = numberOfRecords}, token);
            int[] productIds = response.Message.Ids;
            if (productIds.Length == 0)
            {
                return new List<ProductModel>();
            }
            var res = await _getProducts.GetResponse<GetProductsResponse>
                        (new GetProductsRequest { Ids = productIds });
            return _mapper.Map<List<ProductModel>>(res.Message.Products);
        }
    }
}
