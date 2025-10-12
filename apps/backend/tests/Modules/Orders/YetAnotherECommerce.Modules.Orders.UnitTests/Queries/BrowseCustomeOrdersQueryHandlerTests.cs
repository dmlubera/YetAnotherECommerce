using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Mappers;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Queries
{
    public class BrowseCustomeOrdersQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly IMapper _mapper;
        private readonly BrowseCustomerOrdersQueryHandler _handler;

        public BrowseCustomeOrdersQueryHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapper = new MapperConfiguration(x => x.AddProfile(new OrderProfile())).CreateMapper();
            _handler = new BrowseCustomerOrdersQueryHandler(_orderRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task WhenOrderForCustomerExists_ThenShouldMapToDtos()
        {
            var customerId = Guid.NewGuid();
            var query = new BrowseCustomerOrdersQuery(Guid.NewGuid());
            var products = new List<Order>
            {
                new Order(customerId, new List<OrderItem>()),
                new Order(customerId, new List<OrderItem>())
            };
            _orderRepositoryMock
                .Setup(x => x.BrowseByCustomerAsync(It.IsAny<Guid>()))
                .ReturnsAsync(products);

            var result = await _handler.HandleAsync(query);

            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo(typeof(IReadOnlyList<OrderDto>));
            result.Count.ShouldBe(products.Count);
        }

        [Fact]
        public async Task WhenNoOrdersForCustomerExist_ThenShouldReturnEmptyCollection()
        {
            var query = new BrowseCustomerOrdersQuery(Guid.NewGuid());
            _orderRepositoryMock
                .Setup(x => x.BrowseByCustomerAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var result = await _handler.HandleAsync(query);

            result.ShouldBeEmpty();
        }
    }
}