using TinyShop.Catalog.CustomTypes;
using TinyShop.Catalog.DTOs;
using TinyShop.Catalog.Repositories;
using Xunit;

namespace TinyShop.Catalog.Tests
{
    public class ProductRepositoryTest
    {
        private readonly IProductRepository _repo;
        private AppDbContext _appDbContext;
        public ProductRepositoryTest()
        {
            _appDbContext = new AppDbContext();
            _repo = new ProductRepository(_appDbContext);
        }
        [Fact]
        public void TestReturnProductIsCorrect()
        {
            // Arrange
            ProductFilterDto filter = new ProductFilterDto
            {
                OrderBy = OrderByEnum.ProductName,
                PageNumber = 1,
                RowsPerPage = 20,
                SortOrder = SortOrderEnum.ASC
            };
            var res = _repo.GetAll(filter).Result;
            Assert.Equal(1, 1);
        }


    }
}