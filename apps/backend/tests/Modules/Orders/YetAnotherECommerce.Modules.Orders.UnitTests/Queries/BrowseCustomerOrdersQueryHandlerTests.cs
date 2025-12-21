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

public class BrowseCustomerOrdersQueryHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly BrowseCustomerOrdersQueryHandler _handler;

    public BrowseCustomerOrdersQueryHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _handler = new BrowseCustomerOrdersQueryHandler(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task WhenOrderForCustomerExists_ThenShouldMapToDtos()
    {
        var customerId = Guid.NewGuid();
        var query = new BrowseCustomerOrdersQuery(Guid.NewGuid());
        var products = new List<Order>
        {
            new(customerId, []),
            new(customerId, [])
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
            .ReturnsAsync([]);

        var result = await _handler.HandleAsync(query);

        result.ShouldBeEmpty();
    }
}