using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ResetPassword;

public record ResetPasswordCommand(Guid UserId, string Token, string Password) : ICommand<ResetPasswordResult>;