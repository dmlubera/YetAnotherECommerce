using System.Collections.Generic;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries;

public record BrowseQuery : IQuery<IReadOnlyList<OrderDto>>;