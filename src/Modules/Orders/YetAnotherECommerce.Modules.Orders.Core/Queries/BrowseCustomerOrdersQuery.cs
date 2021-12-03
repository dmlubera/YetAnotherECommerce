using System;
using System.Collections.Generic;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries
{
    public class BrowseCustomerOrdersQuery : IQuery<IList<OrderDto>>
    {
        public Guid CustomerId { get; set; }

        public BrowseCustomerOrdersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}