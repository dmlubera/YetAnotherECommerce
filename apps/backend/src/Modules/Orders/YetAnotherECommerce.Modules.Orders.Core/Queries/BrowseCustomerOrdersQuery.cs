using System;
using System.Collections.Generic;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries;

public class BrowseCustomerOrdersQuery(Guid customerId) : IQuery<IReadOnlyList<OrderDto>>
{
    public Guid CustomerId { get; } = customerId;
}