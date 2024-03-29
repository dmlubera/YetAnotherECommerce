﻿using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions
{
    public class UserNotExistException : YetAnotherECommerceException
    {
        public override string ErrorCode => "user_not_exist";

        public UserNotExistException(Guid id)
            : base($"User with ID: {id} does not exist.")
        {

        }

        public UserNotExistException(string email)
            : base($"User with email: {email} does not exist.")
        {

        }
    }
}