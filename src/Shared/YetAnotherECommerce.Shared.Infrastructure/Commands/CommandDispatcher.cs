using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Shared.Infrastructure.Commands
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command is null) return;

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }
    }
}