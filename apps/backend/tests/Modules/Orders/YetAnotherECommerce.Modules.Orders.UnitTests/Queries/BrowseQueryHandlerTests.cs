using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

namespace YetAnotherECommerce.Modules.Orders.UnitTests.Queries;

public class BrowseQueryHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly BrowseQueryHandler _handler;

    public BrowseQueryHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _handler = new BrowseQueryHandler(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task WhenOrderExists_ThenShouldMapToDtos()
    {
        var customerId = Guid.NewGuid();
        var query = new BrowseQuery();
        var products = new List<Order>
        {
            new(customerId, []),
            new(customerId, [])
        };
        _orderRepositoryMock
            .Setup(x => x.BrowseAsync())
            .ReturnsAsync(products);

        var result = await _handler.HandleAsync(query);

        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo(typeof(IReadOnlyList<OrderDto>));
        result.Count.ShouldBe(products.Count);
    }

    [Fact]
    public async Task WhenNoOrdersExist_ThenShouldReturnEmptyCollection()
    {
        var query = new BrowseQuery();
        _orderRepositoryMock
            .Setup(x => x.BrowseAsync())
            .ReturnsAsync([]);

        var result = await _handler.HandleAsync(query);

        result.ShouldBeEmpty();
    }
}