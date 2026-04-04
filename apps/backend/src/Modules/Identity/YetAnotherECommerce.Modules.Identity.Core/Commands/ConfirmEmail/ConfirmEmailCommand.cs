using System;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ConfirmEmail;

public record ConfirmEmailCommand(Guid UserId, string Token) : ICommand<ConfirmEmailResult>;