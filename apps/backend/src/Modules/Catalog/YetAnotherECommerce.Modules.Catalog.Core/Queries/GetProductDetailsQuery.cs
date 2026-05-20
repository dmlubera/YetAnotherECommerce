using System;
using YetAnotherECommerce.Modules.Catalog.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Catalog.Core.Queries;

public record GetProductDetailsQuery(Guid ProductId) : IQuery<ProductDetailsDto>;