using TinyShop.Catalog.DTOs;
using TinyShop.Contracts;
using TinyShop.Catalog.Extensions;
using AutoMapper;

namespace TinyShop.Catalog.Repositories
{
    public class BreadcrumbsRepository : IBreadcrumbsRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public BreadcrumbsRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<BreadcrumbDto>> Get(GetBreadcrumbsRequest request)
        {
            return await _db.GetBreadcrumbs(request, _mapper);
        }
    }
}
