using System;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;

namespace YetAnotherECommerce.Modules.Identity.Core.Exceptions;

public class UserNotExistException(Guid id) : YetAnotherECommerceException($"User with ID: {id} does not exist.")
{
    public override string ErrorCode => "user_not_exist";
}