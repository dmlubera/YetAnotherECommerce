using System;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Core.Queries;

public record GetProductDetailsQuery(Guid ProductId) : IQuery<ProductDetailsDto>;