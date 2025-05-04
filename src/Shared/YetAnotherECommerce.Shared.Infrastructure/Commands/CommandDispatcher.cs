using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Shared.Infrastructure.Commands;

internal class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }

    public async Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command)
    {
        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await ((Task<TResult>)handlerType
            .GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync))
            ?.Invoke(handler, [command]))!;
    }

    public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.HandleAsync(command); 
    }
}