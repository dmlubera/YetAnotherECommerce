﻿using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Orders.Core.Exceptions
{
    public class OrderDoesNotExistException : YetAnotherECommerceException
    {
        public override string ErrorCode => "order_not_exists";

        public OrderDoesNotExistException(Guid orderId)
            : base($"Order with ID: {orderId} does not exist.")
        {

        }
    }
}