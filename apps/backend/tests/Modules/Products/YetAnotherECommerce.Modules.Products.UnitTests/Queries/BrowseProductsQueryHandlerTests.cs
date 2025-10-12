using AutoMapper;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Mappers;
using YetAnotherECommerce.Modules.Products.Core.Queries;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Entities;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Queries
{
    public class BrowseProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly BrowseProductsQueryHandler _handler;

        public BrowseProductsQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapper = new MapperConfiguration(x => x.AddProfile(new ProductProfile())).CreateMapper();
            _handler = new BrowseProductsQueryHandler(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task WhenProductsExists_ThenShouldMapToDtos()
        {
            var products = new List<Product>
            {
                ProductFixture.Create(),
                ProductFixture.Create()
            };
            _productRepositoryMock
                .Setup(x => x.GetAsync())
                .ReturnsAsync(products);

            var result = await _handler.HandleAsync(new BrowseProductsQuery());

            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo(typeof(IReadOnlyList<ProductDto>));
            result.Count.ShouldBe(products.Count);
        }

        [Fact]
        public async Task WhenNoProductsExist_ThenShouldReturnEmptyCollection()
        {
            _productRepositoryMock
                .Setup(x => x.GetAsync())
                .ReturnsAsync(() => null);

            var result = await _handler.HandleAsync(new BrowseProductsQuery());

            result.ShouldBeEmpty();
        }
    }
}