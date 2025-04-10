using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    public Task HandleAsync(ChangePasswordCommand command)
    {
        throw new NotImplementedException();
    }
}