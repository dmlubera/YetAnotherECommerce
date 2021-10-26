using System;
using System.Collections.Generic;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Core.Queries
{
    public class BrowseCustomerOrdersQuery : IQuery<IList<Order>>
    {
        public Guid CustomerId { get; set; }

        public BrowseCustomerOrdersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}