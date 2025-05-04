using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.Commands;

public interface ICommandDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;

    Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command);
}