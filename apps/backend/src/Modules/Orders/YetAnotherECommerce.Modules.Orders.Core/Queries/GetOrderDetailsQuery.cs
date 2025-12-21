using System;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries;

public record GetOrderDetailsQuery(Guid CustomerId, Guid OrderId) : IQuery<OrderDetailsDto>;